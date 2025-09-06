using Abp;
using Abp.Domain.Repositories;
using Abp.Json;
using Abp.UI;
using JCSoft.WX.Framework.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MQKJ.BSMP.BigRisks.WeChat;
using MQKJ.BSMP.BigRisks.WeChat.WechatPay;
using MQKJ.BSMP.Common.WechatPay;
using MQKJ.BSMP.HttpContextHelper;
using MQKJ.BSMP.MiniappServices;
using MQKJ.BSMP.MiniappServices.Models;
using MQKJ.BSMP.MultiTenancy;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.Orders.Dtos;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Products;
using MQKJ.BSMP.Utils.Extensions;
using MQKJ.BSMP.Utils.Tools;
using MQKJ.BSMP.Utils.Tools.HttpRequestTool;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using MQKJ.BSMP.WeChatPay.Dtos;
using MQKJ.BSMP.WeChatPay.Models;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MQKJ.BSMP.WeChatPay
{
    public class WeChatPayAppService : BSMPAppServiceBase, IWeChatPayAppService
    {
        /// <summary>
        /// 统一下单地址
        /// </summary>
        private const string wechaBasetUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";

        /// <summary>
        /// 微信订单查询地址
        /// </summary>
        private const string QueryUrl = "https://api.mch.weixin.qq.com/pay/orderquery";

        private readonly IRepository<Player, Guid> _playerRepository;

        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<WechatPayNotify, Guid> _notifyRepository;

        private readonly IMiniappService _miniappService;

        private readonly IApiClient _apiClient;

        private readonly IOrderAppService _orderAppService;

        private WechatpublicPlatformConfig _wechatConfig;

        private WechatPayConfig _wechatPayConfig;
        private WechatPubPayConfig _wechatPubPayConfig;

        private readonly IRepository<Order, Guid> _orderRepository;

        public WeChatPayAppService(IRepository<Player, Guid> playerRepository,
            IApiClient apiClient,
            IMiniappService miniappService,
            OrderAppService orderAppService,
            IRepository<Product> productRepository,
            IOptions<WechatpublicPlatformConfig> option,
            IRepository<Order, Guid> orderRepository,
            IOptions<WechatPayConfig> wechatPayConfigOption,
            IOptions<WechatPubPayConfig> wechatPubPayConfigOption,
            IRepository<WechatPayNotify, Guid> notifyRepository)
        {
            _playerRepository = playerRepository;

            _apiClient = apiClient;

            _miniappService = miniappService;

            _orderAppService = orderAppService;

            _productRepository = productRepository;

            _wechatConfig = option.Value;

            _wechatPayConfig = wechatPayConfigOption.Value;
            _wechatPubPayConfig = wechatPubPayConfigOption.Value;

            _orderRepository = orderRepository;
            _notifyRepository = notifyRepository;
        }

        public Task GetId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 微信支付-新接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<MiniProgramPayOutput> Pay(SendPaymentRquestInput input)
        {
            Logger.Debug($"小程序端准备发起支付：{input.ToJsonString()}");

            MiniProgramPayOutput output = null;

            Exception exception = null;

            //var notifyUrl = string.Empty;

            //if (input.GoodsType == GoodsType.RechargeCoin)
            //{
            //    notifyUrl = _wechatPayConfig.NotifyUrl;
            //}
            //else if (input.GoodsType == GoodsType.BuyBigGiftBag)
            //{
            //    notifyUrl = _wechatPayConfig.PropBigNotifyUrl;
            //}

            var tenant = new Tenant();

            var getOpenIdInput = new GetOpenIdInput();

            getOpenIdInput.Code = input.Code;

            input.Out_trade_no = string.Format("{0}{1}{2}", _wechatPayConfig.MchId, DateTime.Now.ToString("yyyyMMddHHmmss"), WeChatPayHelper.BuildRandomStr(6));

            try
            {
                var getOpenIdOutput = new GetOpenIdOutput();

                if (input.ClientType == ClientType.MinProgram)
                {
                    tenant = _miniappService.GetTenant(input.TenantId);
                    getOpenIdInput.AppId = tenant.WechatAppId;
                    getOpenIdInput.AppSecret = tenant.WechatAppSecret;
                }
                else if (input.ClientType == ClientType.PublicAccount)// H5支付
                {
                    getOpenIdInput.AppId = _wechatConfig.AppId;
                    getOpenIdInput.AppSecret = _wechatConfig.Secret;
                    getOpenIdOutput.OpenId = _miniappService.GetOfficeAccountOpenId(getOpenIdInput).OpenId;
                }

                //input.NotifyUlr = _wechatPayConfig.NotifyUrl;
                Logger.Debug($"（Pay）支付回调地址：{_wechatPayConfig.NotifyUrl}，环境：{_wechatPayConfig.Env}");
                var payInput = new MiniProgramPayInput()
                {
                    OpenId = getOpenIdOutput.OpenId == null ? input.OpenId : getOpenIdOutput.OpenId,
                    AppId = getOpenIdInput.AppId,
                    MchId = _wechatPayConfig.MchId,
                    Key = _wechatPayConfig.Key,
                    OutTradeNo = input.Out_trade_no,
                    Body = input.Body,
                    Attach = input.Attach,
                    NotifyUrl = _wechatPayConfig.NotifyUrl,
                    SpbillCreateIp = CustomHttpContext.Current.Connection.RemoteIpAddress.ToString(),
                    TotalFee = input.Totalfee,
                    TradeType = getOpenIdOutput.OpenId == null ? "JSAPI" : "MWEB"
                };
                Logger.Warn($"小程序端微信发起支付：{payInput.ToJsonString()}");
                var result = await WeChatPay(payInput);
                Logger.Debug($"小程序端微信返回参数：{result.ToJsonString()}");

                await _orderAppService.CreateOrder(new CreateOrderInput()
                {
                    OrderNumber = input.Out_trade_no,
                    Payment = Convert.ToDouble(input.Totalfee),
                    TenantId = tenant.Id,
                    ProductId = input.ProductId,
                    PlayerId = input.PlayerId,
                    GoodsType = input.GoodsType,
                    FamilyId = input.FamilyId,
                    PaymentType = PaymentType.MinProgram,
                    PropBagId = input.PropBagId
                });

                output = result;
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            if (exception != null)
            {
                Logger.Error("支付失败：" + exception);

                throw new UserFriendlyException("支付异常，请联系客服人员或稍后再试");
            }

            return output;
        }

        public async Task<MiniProgramPayOutput> H5Pay(SendPaymentRquestInput input)
        {
            Logger.Debug($"H5准备发起支付输入参数：{input.ToJsonString()}");

            MiniProgramPayOutput output = null;

            Exception exception = null;

            var tenant = new Tenant();

            var getOpenIdInput = new GetOpenIdInput();

            getOpenIdInput.Code = input.Code;

            input.Out_trade_no = string.Format("{0}{1}{2}", _wechatPubPayConfig.MchId, DateTime.Now.ToString("yyyyMMddHHmmss"), WeChatPayHelper.BuildRandomStr(6));

            try
            {
                var getOpenIdOutput = new GetOpenIdOutput();


                getOpenIdInput.AppId = _wechatConfig.AppId;
                getOpenIdInput.AppSecret = _wechatConfig.Secret;
                getOpenIdOutput.OpenId = input.OpenId;

                Logger.Warn($"客户端IP：{CustomHttpContext.Current.Connection.RemoteIpAddress.ToString()}");

                //input.NotifyUlr = _wechatPayConfig.NotifyUrl;
                Logger.Debug($"（Pay）H5支付配置信息：{_wechatPubPayConfig.ToJsonString()}");
                var result = await WeChatPay(new MiniProgramPayInput()
                {
                    OpenId = input.OpenId,
                    AppId = getOpenIdInput.AppId,
                    MchId = _wechatPubPayConfig.MchId,
                    Key = _wechatPubPayConfig.Key,
                    OutTradeNo = input.Out_trade_no,
                    Body = input.Body,
                    Attach = input.Attach,
                    NotifyUrl = _wechatPubPayConfig.NotifyUrl,//PayInfo.NotifyUrl,
                    SpbillCreateIp = CustomHttpContext.Current.Connection.RemoteIpAddress.ToString(),
                    TotalFee = input.Totalfee,
                    TradeType = "JSAPI"
                });
                Logger.Debug($"H5准备发起支付微信返回参数：{result.ToJsonString()}");

                tenant = _miniappService.GetTenant(input.TenantId);

                if (!input.FamilyId.HasValue)
                {
                    throw new Exception("参数不能为空");
                }

                await _orderAppService.CreateOrder(new CreateOrderInput()
                {
                    OrderNumber = input.Out_trade_no,
                    Payment = Convert.ToDouble(input.Totalfee),
                    TenantId = tenant.Id,
                    ProductId = input.ProductId,
                    PlayerId = input.PlayerId,
                    GoodsType = input.GoodsType,
                    FamilyId = input.FamilyId,
                    PaymentType = PaymentType.WeCahtPub
                });

                output = result;
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            if (exception != null)
            {
                Logger.Error("支付失败：" + exception);

                throw new UserFriendlyException("支付异常，请联系客服人员或稍后再试");
            }

            return output;
        }

        //public

        /// <summary>
        ///     微信支付
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected Task<MiniProgramPayOutput> WeChatPay(MiniProgramPayInput input)
        {
            //try
            //{
                var appPayOutput = WeChatPayApi.MiniProgramPay(input);

                return Task.FromResult(appPayOutput);
            //}
            //catch (Exception ex)
            //{
            //    throw new UserFriendlyException(ex.Message);
            //}
        }

        public async Task<SendPaymentRquestOutput> SendPaymentRquest(SendPaymentRquestInput input)
        {
            var output = new SendPaymentRquestOutput();

            var productEntity = await _productRepository.FirstOrDefaultAsync(x => x.Id == input.ProductId);

            if (productEntity == null)
            {
                return new SendPaymentRquestOutput() { ErrMsg = "商品不存在" };
            }

            var tenant = new Tenant();
            try
            {
                tenant = await GetCurrentTenantAsync();

                var getOpenIdResponse = _miniappService.GetOpenId(new GetOpenIdInput
                {
                    AppId = tenant.WechatAppId,
                    AppSecret = tenant.WechatAppSecret,
                    Code = input.Code
                });

                Dictionary<string, string> paramDic = new Dictionary<string, string>();
                //小程序AppId
                paramDic.Add("appid", tenant.WechatAppId);
                //附加数据
                paramDic.Add("attach", PayInfo.Attach);
                //商品描述
                paramDic.Add("body", PayInfo.Body);
                //商户号
                paramDic.Add("mch_id", _wechatPayConfig.MchId);

                string nonceStr = WeChatPayHelper.GetNoncestr();
                //随机串
                paramDic.Add("nonce_str", nonceStr);
                //通知地址(异步接收支付结果通知回调地址) 必须是外网能访问且不能携带参数
                paramDic.Add("notify_url", _wechatPayConfig.NotifyUrl);
                //openid 每个用户唯一标识
                paramDic.Add("openid", getOpenIdResponse.OpenId);
                //生成订单号
                string trade_no = string.Format("{0}{1}{2}", _wechatPayConfig.MchId, DateTime.Now.ToString("yyyyMMddHHmmss"),
                            WeChatPayHelper.BuildRandomStr(6));
                //订单号
                paramDic.Add("out_trade_no", trade_no);
                //终端Id
                paramDic.Add("spbill_create_ip", CustomHttpContext.Current.Connection.RemoteIpAddress.ToString());
                //金额
                paramDic.Add("total_fee", Convert.ToDouble(productEntity.Price * 100).ToString());
                //交易类型
                paramDic.Add("trade_type", PayInfo.TradeType);
                var sign = WeChatPayHelper.GetSignInfo(paramDic, _wechatPayConfig.Key);
                //签名
                paramDic.Add("sign", sign);

                var result = CustomHttpRequest.PostHttpResponse(wechaBasetUrl, XmlHelper.CreateXmlParam(paramDic));

                string strCode = XmlHelper.GetXmlValue(result, "return_code");

                string strMsg = XmlHelper.GetXmlValue(result, "return_msg");

                if (strCode == "SUCCESS")
                {
                    await _orderAppService.CreateOrder(new CreateOrderInput()
                    {
                        OrderNumber = trade_no,
                        Payment = productEntity.Price,
                        TenantId = tenant.Id,
                        ProductId = input.ProductId,
                        PlayerId = input.PlayerId
                    });

                    var timestamp = WeChatPayHelper.GetTimestamp();
                    //再次签名
                    string nonecStr = nonceStr;
                    string timeStamp = timestamp;
                    string package = "prepay_id=" + XmlHelper.GetXmlValue(result, "prepay_id");
                    Dictionary<string, string> singInfo = new Dictionary<string, string>();
                    singInfo.Add("appId", tenant.WechatAppId);
                    singInfo.Add("nonceStr", nonecStr);
                    singInfo.Add("package", package);
                    singInfo.Add("signType", PayInfo.SignType);
                    singInfo.Add("timeStamp", timeStamp);
                    //返回参数
                    output.TimeStamp = timestamp;
                    output.SignType = "MD5";
                    output.NonceStr = nonceStr;
                    output.Package = package;
                    output.TradeNo = trade_no;
                    output.PaySign = WeChatPayHelper.GetSignInfo(singInfo, _wechatPayConfig.Key);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("发起支付报错啦", ex);
                output.ErrMsg = "服务器出错";
            }
            return output;
        }

        /// <summary>
        /// 支付成功回调-新的接口
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetPayNotify()
        {
            #region 原代码
            //Logger.Warn("新的支付回调");
            //var strResult = string.Empty;

            //var xmlResult = GetPostStr();

            //Logger.Warn("新的支付回调结果" + xmlResult);

            //var notifyOutput = WeChatPayApi.PayNotifyHandler(xmlResult);
            //var getSign = WeChatPayHelper.GetSignInfo(XmlHelper.GetFromXml(xmlResult), _wechatPayConfig.Key);
            //if (notifyOutput.Sign == getSign)
            //{
            //    var orderEntity = await _orderAppService.QueryOrder(new QueryOrderInput()
            //    {
            //        OrderNumber = notifyOutput.OutTradeNo
            //    });
            //    //校验订单是否存在
            //    if (orderEntity != null)
            //    {
            //        await _orderAppService.UpdateOrderState(
            //            new UpdateOrderStateInput(notifyOutput.IsSuccess(),
            //            notifyOutput.TotalFee.ToInt32(),
            //            notifyOutput.SettlementTotalFee.ToInt32(),
            //            xmlResult)
            //            {
            //                OrderNumber = notifyOutput.OutTradeNo,
            //                TransactionId = notifyOutput.TransactionId,
            //                EndTime = notifyOutput.TimeEnd,
            //            });
            //        //3.返回一个xml格式的结果给微信服务器
            //        strResult = WeChatPayHelper.GetReturnXml("SUCCESS", "OK");
            //    }
            //    else
            //    {
            //        strResult = WeChatPayHelper.GetReturnXml("FAIL", "支付结果中微信订单号数据库不存在！");
            //    }
            //}
            //else
            //{
            //    strResult = WeChatPayHelper.GetReturnXml("FAIL", "签名不一致!");
            //}
            ////else
            ////{
            ////    strResult = WeChatPayHelper.GetReturnXml("FAIL", "支付通知失败!");
            ////}

            //Logger.Warn("新接口返回微信服务器的结果" + strResult);

            //return strResult;
            #endregion

            #region 新代码
            string strResult = string.Empty;
            try
            {
                //获取微信参数
                string strXML = GetPostStr();

                Logger.Warn($"付款通知获取的微信服务器返回的参数：{strXML}");
                var payResultResponse = strXML.XmlToObject<WechatPayResultResponse>();

                //是否请求成功
                if (payResultResponse != null && payResultResponse.IsReturnSuccess)
                {
                    var payResult = payResultResponse.IsResultSuccess;
                    //判断是否支付成功
                    //获得签名
                    string getSign = payResultResponse.Sign;
                    //进行签名
                    string sign = WeChatPayHelper.GetSignInfo(XmlHelper.GetFromXml(strXML), _wechatPayConfig.Key);
                    if (sign == getSign)
                    {
                        //校验订单信息
                        var wxOrderNum = payResultResponse.TransactionId; //微信订单号
                        var orderNum = payResultResponse.OutTradeNo;    //商户订单号
                        var orderTotal = payResultResponse.TotalFee;
                        var openid = payResultResponse.OpenId;

                        var orderEntity = await _orderAppService.QueryOrder(new QueryOrderInput()
                        {
                            OrderNumber = orderNum
                        });
                        //校验订单是否存在
                        if (orderEntity != null)
                        {
                            if (orderEntity.State == OrderState.Paid)
                            {
                                strResult = WeChatPayHelper.GetReturnXml("SUCCESS", "OK");
                            }
                            else
                            {
                                //await _orderAppService.UpdateOrderState(
                                //    new UpdateOrderStateInput(payResult,
                                //        payResultResponse.TotalFee,
                                //        payResultResponse.SettlementTotalFee,
                                //        strXML)
                                //    {
                                //        //PaymentData = strXML,
                                //        OrderNumber = orderNum,
                                //        TransactionId = wxOrderNum,
                                //        EndTime = payResultResponse.TimeEnd
                                //    });

                                //3.返回一个xml格式的结果给微信服务器
                                strResult = WeChatPayHelper.GetReturnXml("SUCCESS", "OK");
                            }
                        }
                        else
                        {
                            strResult = WeChatPayHelper.GetReturnXml("FAIL", "支付结果中微信订单号数据库不存在！");
                        }
                    }
                }
                else
                {
                    strResult = WeChatPayHelper.GetReturnXml("FAIL", "签名不一致!");
                }

                //else
                //{
                //    strResult = WeChatPayHelper.GetReturnXml("FAIL", "支付通知失败!");
                //}
            }
            catch (Exception ex)
            {
                strResult = WeChatPayHelper.GetReturnXml("FAIL", ex.Message);
                Logger.Error($"支付通知接口报错:{ex}");
            }
            Logger.Debug($"（GetPayNotify）支付回调地址：{_wechatPayConfig.NotifyUrl}，环境：{_wechatPayConfig.Env}");
            Logger.Warn($"支付通知返回微信服务端的数据：{strResult}");
            return strResult;
            #endregion

        }

        /// <summary>
        /// 支付成功回调-原来的接口(不用)
        /// </summary>
        /// <returns></returns>
        //public async Task<string> getNotifyInfo()
        //{
        //    string strResult = string.Empty;

        //    try
        //    {
        //        //获取微信参数
        //        string strXML = GetPostStr();

        //        Logger.Warn($"付款通知获取的微信服务器返回的参数：{strXML}");
        //        //是否请求成功
        //        if (XmlHelper.GetXmlValue(strXML, "return_code") == "SUCCESS")
        //        {
        //            var payResult = XmlHelper.GetXmlValue(strXML, "result_code") == "SUCCESS";
        //            //判断是否支付成功
        //            //获得签名
        //            string getSign = XmlHelper.GetXmlValue(strXML, "sign");
        //            //进行签名
        //            string sign = WeChatPayHelper.GetSignInfo(XmlHelper.GetFromXml(strXML), _wechatPayConfig.Key);
        //            if (sign == getSign)
        //            {
        //                //校验订单信息
        //                string wxOrderNum = XmlHelper.GetXmlValue(strXML, "transaction_id"); //微信订单号
        //                string orderNum = XmlHelper.GetXmlValue(strXML, "out_trade_no");    //商户订单号
        //                string orderTotal = XmlHelper.GetXmlValue(strXML, "total_fee");
        //                string openid = XmlHelper.GetXmlValue(strXML, "openid");

        //                var orderEntity = await _orderAppService.QueryOrder(new QueryOrderInput()
        //                {
        //                    OrderNumber = orderNum
        //                });
        //                //校验订单是否存在
        //                if (orderEntity != null)
        //                {
        //                    await _orderAppService.UpdateOrderState(
        //                        new WechatPayUpdateOrderStateInput(payResult,
        //                            XmlHelper.GetXmlValue(strXML, "total_fee").ToInt32(),
        //                            XmlHelper.GetXmlValue(strXML, "settlement_total_fee").ToInt32(),
        //                            strXML)
        //                        {
        //                            OrderNumber = orderNum,
        //                            TransactionId = XmlHelper.GetXmlValue(strXML, "transaction_id"),
        //                            EndTime = XmlHelper.GetXmlValue(strXML, "time_end")
        //                        });

        //                    //3.返回一个xml格式的结果给微信服务器
        //                    strResult = WeChatPayHelper.GetReturnXml("SUCCESS", "OK");
        //                }
        //                else
        //                {
        //                    strResult = WeChatPayHelper.GetReturnXml("FAIL", "支付结果中微信订单号数据库不存在！");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            strResult = WeChatPayHelper.GetReturnXml("FAIL", "签名不一致!");
        //        }

        //        //else
        //        //{
        //        //    strResult = WeChatPayHelper.GetReturnXml("FAIL", "支付通知失败!");
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error($"支付通知接口报错:{ex}");
        //    }

        //    Logger.Warn($"支付通知返回微信服务端的数据：{strResult}");
        //    return strResult;
        //}



        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public async Task<QueryOrderOutput> QueryOrder(QueryOrderStateInput input)
        //{
        //    var result = new QueryOrderOutput();
        //    try
        //    {
        //        //查询网关的订单状态
        //        var tenant = _miniappService.GetTenant(input.TenantId);

        //        var orderEntity = await _orderRepository.FirstOrDefaultAsync(o => o.OrderNumber == input.OutTradNo);
        //        Logger.Debug($"订单查询，WeChatPayAppService,QueryOrder,orderEntity.State :{orderEntity?.State }");
        //        result.BussinessState = orderEntity.BussinessState;
        //        //if (orderEntity.State == OrderState.Paid)
        //        //{
        //        //    result.IsUpdateOrder = true;
        //        //}
        //        //else
        //        //{
        //        var orderQueryOutput = WeChatPayApi.OrderQuery(new OrderQueryInput()
        //        {
        //            AppId = tenant.WechatAppId,
        //            MchId = _wechatPayConfig.MchId,
        //            Key = _wechatPayConfig.Key,
        //            NonceStr = WeChatPayHelper.GetNoncestr(),
        //            OutTradeNo = input.OutTradNo,
        //            TransactionId = input.TransactionId,
        //        });

        //        result.OrderPayResult = orderQueryOutput.IsSuccess();
        //        var updateRequest = new WechatPayUpdateOrderStateInput(
        //                    orderQueryOutput.IsSuccess(),
        //                    orderQueryOutput.TotalFee,
        //                    orderQueryOutput.SettlementTotalFee,
        //                    JsonConvert.SerializeObject(orderQueryOutput))
        //        {
        //            EndTime = orderQueryOutput.TimeEnd,
        //            OrderNumber = orderQueryOutput.OutTradeNo,
        //            TransactionId = orderQueryOutput.TransactionId,
        //        };

        //        result.OrderNumber = updateRequest.OrderNumber;
        //        result.PayAmount = updateRequest.PayAmount;
        //        Logger.Debug($"订单查询，WechatAppService,QueryOrder,orderQueryOutput.IsSuccess() :{orderQueryOutput.IsSuccess() }");
        //        if (orderQueryOutput.IsSuccess())
        //        {
        //            await _orderAppService.UpdateOrderState(
        //                       new WechatPayUpdateOrderStateInput(orderQueryOutput.IsSuccess(),
        //                           orderQueryOutput.TotalFee,
        //                           orderQueryOutput.TotalFee,
        //                           orderQueryOutput.ToJsonString())
        //                       {
        //                           PaymentData = orderQueryOutput.ToJsonString(),
        //                           OrderNumber = orderQueryOutput.OutTradeNo,
        //                           TransactionId = orderQueryOutput.TransactionId,
        //                           EndTime = orderQueryOutput.TimeEnd
        //                       });
        //        }
        //        //}

        //        //支付成功的话 更新订单状态
        //        //if (result.OrderPayResult)
        //        //{
        //        //    await _orderAppService.UpdateOrderState(updateRequest);
        //        //}
        //    }
        //    catch (Exception exp)
        //    {
        //        result.OrderPayResult = false;
        //        Logger.Error($"查询订单失败：{exp}");
        //    }

        //    return result;
        //}

        /// <summary>
        /// 查询订单(不用)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public async Task<string> QueryOrderState(QueryOrderStateInput input)
        //{
        //    var result = string.Empty;

        //    var tenant = new Tenant();
        //    try
        //    {
        //        tenant = await GetCurrentTenantAsync();

        //        Dictionary<string, string> paramDic = new Dictionary<string, string>();
        //        paramDic.Add("appid", tenant.WechatAppId);
        //        paramDic.Add("mch_id", _wechatPayConfig.MchId);
        //        paramDic.Add("nonce_str", WeChatPayHelper.GetNoncestr());
        //        paramDic.Add("out_trade_no", input.OutTradNo);
        //        var sign = WeChatPayHelper.GetSignInfo(paramDic, _wechatPayConfig.Key);
        //        //签名
        //        paramDic.Add("sign", sign);

        //        var response = CustomHttpRequest.PostHttpResponse(QueryUrl, XmlHelper.CreateXmlParam(paramDic));

        //        Logger.Warn($"查询订单结果:{response}");

        //        string returnCode = XmlHelper.GetXmlValue(response, "return_code");

        //        if (returnCode == "SUCCESS")
        //        {
        //            string state = XmlHelper.GetXmlValue(response, "trade_state");
        //            var payResult = XmlHelper.GetXmlValue(response, "result_code") == "SUCCESS" && state == "SUCCESS"; var orderEntity = await _orderAppService.QueryOrder(new QueryOrderInput()
        //            {
        //                OrderNumber = input.OutTradNo
        //            });
        //            if (orderEntity.State == OrderState.UnPaid && orderEntity.Payment >= 10)
        //            {
        //                await _orderAppService.UpdateOrderState(
        //                    new WechatPayUpdateOrderStateInput(payResult,
        //                        XmlHelper.GetXmlValue(response, "total_fee").ToInt32(),
        //                        XmlHelper.GetXmlValue(response, "settlement_total_fee").ToInt32(),
        //                        response
        //                    )
        //                    {
        //                        OrderNumber = input.OutTradNo,
        //                        TransactionId = XmlHelper.GetXmlValue(response, "transaction_id"),
        //                        EndTime = XmlHelper.GetXmlValue(response, "time_end")
        //                    });
        //            }

        //            result = state;
        //        }
        //        if (returnCode == "FAIL")
        //        {
        //            return XmlHelper.GetXmlValue(response, "err_code");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("查询订单失败：" + ex);
        //    }
        //    return result;
        //}

        /// <summary>
        /// 获得Post过来的数据
        /// </summary>
        /// <returns></returns>
        public static string GetPostStr()
        {
            var inputBody = string.Empty;
            using (var reader = new System.IO.StreamReader(CustomHttpContext.Current.Request.Body, System.Text.Encoding.UTF8))
            {
                inputBody = reader.ReadToEnd();
            }
            return inputBody;
            //Int32 intLen = Convert.ToInt32(CustomHttpContext.Current.Request.Body.Length);
            //byte[] b = new byte[intLen];
            //CustomHttpContext.Current.Request.Body.Read(b, 0, intLen);
            //return Encoding.UTF8.GetString(b);
        }

        /// <summary>
        /// 查询微信订单状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OrderQueryOutput QueryWechatPayResult(QueryOrderStateInput input)
        {
            var tenant = _miniappService.GetTenant(input.TenantId);
            var minMch = _wechatPayConfig.MchId;
            var pubMch = _wechatPubPayConfig.MchId;
            var temp = input.OutTradNo.Substring(0, minMch.Length);
            var mch = temp == minMch ? minMch : pubMch;
            var key = temp == minMch ? _wechatPayConfig.Key : _wechatPubPayConfig.Key;
            return WeChatPayApi.OrderQuery(new OrderQueryInput()
            {
                AppId = tenant.WechatAppId,
                MchId = mch,
                Key = key,
                NonceStr = WeChatPayHelper.GetNoncestr(),
                OutTradeNo = input.OutTradNo,
                TransactionId = input.TransactionId,
            });
        }

        public async Task<WechatNotifyResponse> WechatPayNotify()
        {
            var xml = GetPostStr();
            // var xml = @"<xml><return_code><![CDATA[SUCCESS]]></return_code> <return_msg><![CDATA[OK]]></return_msg> <appid><![CDATA[wx7db4778518a1e650]]></appid> <mch_id><![CDATA[1520917151]]></mch_id> <nonce_str><![CDATA[tXOJ2bPhaDB5f1w8]]></nonce_str> <sign><![CDATA[2C188572707946FD7B475DA8FD5E5D30]]></sign> <result_code><![CDATA[SUCCESS]]></result_code> <openid><![CDATA[ocOAQ5feuiBqSblvF3VHUMOHVu0o]]></openid> <is_subscribe><![CDATA[N]]></is_subscribe> <trade_type><![CDATA[JSAPI]]></trade_type> <bank_type><![CDATA[CFT]]></bank_type> <total_fee>1</total_fee> <fee_type><![CDATA[CNY]]></fee_type> <transaction_id><![CDATA[4200000251201902031869585250]]></transaction_id> <out_trade_no><![CDATA[152091715120190203111352748141]]></out_trade_no> <attach><![CDATA[微信支付信息]]></attach> <time_end><![CDATA[20190203111400]]></time_end> <trade_state><![CDATA[SUCCESS]]></trade_state> <cash_fee>1</cash_fee> <trade_state_desc><![CDATA[支付成功]]></trade_state_desc> </xml>";
            try
            {
                var result = xml.XmlToObject<WechatNotifyResponse>()
                    .CheckNull($"获取通知失败,请求报文:{xml}");

                await CheckNotifyResult(result, xml);
                await SaveNotifyResult(xml, result);
                return result;
            }
            catch (Exception ex)
            {
                await SaveNotifyResult(xml, null, ex.Message);
            }

            return null;
        }

        private Task SaveNotifyResult(String xml, WechatNotifyResponse result, string errorMessage = "")
        {
            return _notifyRepository.InsertAsync(new WechatPayNotify
            {
                CreationTime = DateTime.Now,
                ErrorMessage = errorMessage,
                NotifyData = xml,
                OutTradeNo = result?.OutTradeNo,
                TransactionId = result?.TransactionId
            });
        }

        private Task CheckNotifyResult(WechatNotifyResponse result, string xml)
        {
            Logger.Debug($"验证支付结果：{result.ToJsonString()}");
            var minSign = WeChatPayHelper.GetSignInfo(XmlHelper.GetFromXml(xml), _wechatPayConfig.Key);
            var pubSign = WeChatPayHelper.GetSignInfo(XmlHelper.GetFromXml(xml), _wechatPubPayConfig.Key);
            if (minSign == result.Sign || pubSign == result.Sign)
            {
                return Task.CompletedTask;
            }
            else
            {
                throw new Exception($"签名不一致,请检查");
            }
        }
    }
}