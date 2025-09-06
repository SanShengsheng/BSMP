
using Abp.Domain.Repositories;
using Abp.Json;
//using Aop.Api;
//using Aop.Api.Domain;
//using Aop.Api.Request;
//using Aop.Api.Response;
//using Aop.Api.Util;
using Alipay.AopSdk.Core;
using Alipay.AopSdk.Core.Domain;
using Alipay.AopSdk.Core.Request;
using Alipay.AopSdk.Core.Response;
using Alipay.AopSdk.Core.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Common.Pay.AliPay;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.Pay.Alipay.Dtos;
using MQKJ.BSMP.Utils.Extensions;
using MQKJ.BSMP.Utils.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Alipay
{
    public class AlipayAppService : BSMPAppServiceBase, IAlipayAppService
    {
        private readonly IOrderAppService _orderAppService;
        private readonly IRepository<Family> _familyRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly IOptions<AliPayConfig> _alipayConfig;
        private readonly IRepository<CoinRechargeRecord, Guid> _coinRechargeRecordRepository;
        // 静态变量
        // 支付宝网关
        //public static string _gatewayUrl = "https://openapi.alipay.com/gateway.do";
        // 签名方式
        public static string _sign_type = "RSA2";
        // 编码格式
        public static string _charset = "UTF-8";

        private static string _PRIVATE_KEY_NAME = "rsa_private_key.pem";
        private static string _PUBLIC_KEY_NAME = "rsa_public_key.pem";
        private static string _KEY_PATH = "api_certificate/aliPay";

        public AlipayAppService(
        OrderAppService orderAppService,
        IRepository<Family> familyRepository,
         IRepository<Order, Guid> orderRepository,
         IHostingEnvironment hostingEnvironment,
         IOptions<AliPayConfig> alipayCOnfig,
         IRepository<CoinRechargeRecord, Guid> coinRechargeRecordRepository
        )
        {
            _orderAppService = orderAppService;
            _orderRepository = orderRepository;
            _hostingEnvironment = hostingEnvironment;
            _alipayConfig = alipayCOnfig;
            _coinRechargeRecordRepository = coinRechargeRecordRepository;
            _familyRepository = familyRepository;
        }
        public async Task<string> Pay(AliPayH5Input input)
        {
            string privateCertPath = GetPrivateCert();
            string publicCertPath = GetPublicCert();

            DefaultAopClient client = new DefaultAopClient(_alipayConfig.Value.Gateway_Url, _alipayConfig.Value.App_ID, privateCertPath, "json", "1.0", _sign_type, publicCertPath, _charset, !_alipayConfig.Value.IsFromLocalFile);

            var tenant = await GetCurrentTenantAsync();
            if (tenant == null)
            {
                var tenantId = Convert.ToInt32(HttpContextHelper.CustomHttpContext.Current.Request.Headers["Abp-TenantId"]);
                tenant = await TenantManager.GetByIdAsync(tenantId);
            }
            var family = await _familyRepository.GetAllIncluding().Include(s => s.Father).Include(s => s.Mother).Include(s => s.Babies).FirstOrDefaultAsync(s => s.Id == input.FamilyId);
            if (family.FatherId != input.PlayerId && input.PlayerId != family.MotherId)
            {
                throw new ArgumentException($"玩家编号参数无效！");
            }
            // 外部订单号，商户网站订单系统中唯一的订单号
            string out_trade_no = WeChatPayHelper.GenerateOutTradeNo();
            // 创建订单
            var orderId = await _orderRepository.InsertAndGetIdAsync(new Order()
            {
                OrderNumber = out_trade_no,
                Payment = Convert.ToDouble(input.Totalfee),
                TenantId = tenant.Id,
                ProductId = input.ProductId,
                PlayerId = input.PlayerId,
                GoodsType = input.GoodsType,
                FamilyId = input.FamilyId,
                PaymentType = PaymentType.AliPay,
                App_ID = _alipayConfig.Value.App_ID,
                Seller_ID = _alipayConfig.Value.Seller_ID,
                State = OrderState.UnPaid,
            });

            // 订单名称
            string subject = "购买金币";
            // 付款金额
            string total_amout = input.Totalfee.ToString();

            // 商品描述
            string body = $"购买{input.ProductAmount}个金币";
            // 支付中途退出返回商户网站地址
            string quit_url = $"{_alipayConfig.Value.Index_Url}?familyId={input.FamilyId}&playerId={input.PlayerId}&babyId={family.LatestBaby.Id}";

            // 组装业务参数model
            AlipayTradeWapPayModel model = new AlipayTradeWapPayModel();
            model.Body = body;
            model.Subject = subject;
            model.TotalAmount = total_amout;
            model.OutTradeNo = out_trade_no;
            model.ProductCode = "QUICK_WAP_WAY";
            model.QuitUrl = quit_url;

            AlipayTradeWapPayRequest request = new AlipayTradeWapPayRequest();
            // 设置支付完成同步回调地址
            var return_url = $"{_alipayConfig.Value.Return_Url}?familyId={input.FamilyId}&playerId={input.PlayerId}&orderId={orderId}&goldCoin={input.ProductAmount}";
            Console.WriteLine($"===============return_url:{return_url}");
            request.SetReturnUrl(return_url);
            // 设置支付完成异步通知接收地址
            request.SetNotifyUrl(_alipayConfig.Value.Notify_Url);
            // 将业务model载入到request
            request.SetBizModel(model);

            AlipayTradeWapPayResponse response = null;
            response = client.PageExecute(request, null, "post");
            Console.WriteLine($"response.body is :{response.Body}");
            return response.Body;
        }

        private string GetPublicCert()
        {
            if (_alipayConfig.Value.IsFromLocalFile)
            {
                var path = Path.Combine(_hostingEnvironment.WebRootPath, _KEY_PATH, _alipayConfig.Value.App_ID, _PUBLIC_KEY_NAME);
                return GetCert(path);
            }
            else
            {
                return _alipayConfig.Value.PublicKey;
            }
        }
        private string GetCert(string path)
        {
            using (System.IO.FileStream fs = System.IO.File.OpenRead(path))
            {
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                var cert = Encoding.UTF8.GetString(data);
                return cert;
            }
        }

        private string GetPrivateCert()
        {
            if (_alipayConfig.Value.IsFromLocalFile)
            {
                var path = Path.Combine(_hostingEnvironment.WebRootPath, _KEY_PATH, _alipayConfig.Value.App_ID, _PRIVATE_KEY_NAME);
                return GetCert(path);
            }
            else
            {
                return _alipayConfig.Value.PrivateKey;
            }
        }

        /// <summary>
        /// 异步返回结果验证签名
        /// 参考网址：https://docs.open.alipay.com/203/105286#s6
        /// 程序执行完后必须打印输出“success”（不包含引号）。如果商户反馈给支付宝的字符不是success这7个字符，支付宝服务器会不断重发通知，直到超过24小时22分钟。一般情况下，25小时以内完成8次通知（通知的间隔频率一般是：4m,10m,10m,1h,2h,6h,15h）；
        /// </summary>
        /// <returns></returns>
        public async Task<NotifyResultCheckSignOutput> NotifyResultCheckSign(AliPayNotifyRsultAsyncDto input)
        {
            var form = HttpContextHelper.CustomHttpContext.Current.Request.Form.ToDictionary(s => s.Key, s => s.Value.ToString());
            var response = new NotifyResultCheckSignOutput();
            Console.WriteLine($"NotifyResultCheckSign request param is :{input.ToJsonString()}");
            Logger.Debug($"NotifyResultCheckSign request param is :{input.ToJsonString()}");
            var order = await _orderRepository.GetAllIncluding().Include(s => s.Family).Include(s => s.Player).FirstOrDefaultAsync(s => s.OrderNumber == input.out_trade_no && s.PaymentType == PaymentType.AliPay);
            // 过滤重复的通知结果
            order.CheckNull($"支付宝异步通知，订单不存在！{input.out_trade_no}");
            string publicCertPath = GetPublicCert();
            //var checkResult = AlipaySignature.RSACheckV1(form, publicCertPath, _charset, _sign_type, true);
            var dic = GetRequestPost();
            var checkResult = AlipaySignature.RSACheckV1(dic, publicCertPath, _charset, _sign_type, !_alipayConfig.Value.IsFromLocalFile);
            //第五步：需要严格按照如下描述校验通知数据的正确性。
            Console.WriteLine($"======>RSACheckV1:{ checkResult};");
            if (!checkResult)
            {
                throw new ApplicationException($"验证签名不通过！");
            }
            else if (order.Payment != Convert.ToDouble(input.total_amount))
            {
                throw new ApplicationException($"支付宝异步通知，订单实际金额不一致！{input.total_amount}");
            }
            else if (order.Seller_ID != input.seller_id)
            {
                throw new ApplicationException($"支付宝异步通知，商户（操作方）编号不一致！{input.seller_id}");
            }
            else if (order.App_ID != input.app_id)
            {
                throw new ApplicationException($"支付宝异步通知，app_id与商户编号不一致 ！{input.app_id}");
            }

            if (order.State != OrderState.UnPaid || input.trade_status != "TRADE_SUCCESS")
            {
                response.Order = order;
                return response;
            }
            else if (input.trade_status == "TRADE_SUCCESS" && order.State == OrderState.UnPaid)
            {
                response.Status = true;
                response.Order = order;
                response.Notify = input;
            }

            return response;
            //1、商户需要验证该通知数据中的out_trade_no是否为商户系统中创建的订单号，2、判断total_amount是否确实为该订单的实际金额（即商户订单创建时的金额），3、校验通知中的seller_id（或者seller_email) 是否为out_trade_no这笔单据的对应的操作方（有的时候，一个商户可能有多个seller_id / seller_email），4、验证app_id是否为该商户本身。上述1、2、3、4有任何一个验证不通过，则表明本次通知是异常通知，务必忽略。在上述验证通过后商户必须根据支付宝不同类型的业务通知，正确的进行不同的业务处理，并且过滤重复的通知结果数据。在支付宝的业务通知中，只有交易通知状态为TRADE_SUCCESS或TRADE_FINISHED时，支付宝才会认定为买家付款成功。
            //注意：
            //    状态TRADE_SUCCESS的通知触发条件是商户签约的产品支持退款功能的前提下，买家付款成功；
            //    交易状态TRADE_FINISHED的通知触发条件是商户签约的产品不支持退款功能的前提下，买家付款成功；或者，商户签约的产品支持退款功能的前提下，交易已经成功并且已经超过可退款期限。

        }
        private Dictionary<string, string> GetRequestPost()
        {
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            var request = HttpContextHelper.CustomHttpContext.Current.Request;
            ICollection<string> requestItem = request.Form.Keys;
            foreach (var item in requestItem)
            {
                sArray.Add(item, request.Form[item]);

            }
            return sArray;

        }
        /// <summary>
        /// 获取充值结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AliPayResultOutput> GetPayResultAsync(GetPayResultAsyncInput input)
        {
            //var family =  await _familyRepository.GetAsync(input.FamilyId);
            var coinRecord = await _coinRechargeRecordRepository.FirstOrDefaultAsync(s => s.OrderId == input.OrderId && s.OrderId != null && s.FamilyId == input.FamilyId);
            var output = new AliPayResultOutput
            {
                Family = null,
                CoinRechargeRecord = coinRecord,
                GoldCoin = input.GoldCoin
            };
            return output;
        }
    }
}
