using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Pay;
using MQKJ.BSMP.Utils.Crypt;
using MQKJ.BSMP.Utils.Extensions;
using Newtonsoft.Json;
using RestSharp;
using StackExchange.Profiling.Internal;
using System;
using System.Threading.Tasks;
using System.Web;

namespace MQKJ.BSMP.AliPay
{
    /// <summary>
    /// 支付宝支付
    /// dealer_id（商户代码）00244818
    /// broker_id（代征主体）yiyun73（云账户）
    /// </summary>
    public class YunZhangHuApplicationService : BSMPAppServiceBase, IYunZhangHuApplicationService
    {
        #region 接口地址

        /// <summary>
        /// 查询日订单文件
        /// </summary>
        private const string _queryDayOrderUrl = "https://api-jiesuan.yunzhanghu.com/api/dataservice/v1/order/downloadurl";
        /// <summary>
        /// 支付宝支付
        /// </summary>
        private const string _aliPayUrl = "https://api-jiesuan.yunzhanghu.com/api/payment/v1/order-alipay";
        /// <summary>
        /// 银行卡支付
        /// </summary>
        private const string _unionPayUrl = "https://api-jiesuan.yunzhanghu.com/api/payment/v1/order-realtime";
        /// <summary>
        /// 银行卡三要素验证
        /// </summary>
        private const string _verifyBankcardThreeFactorUrl = "https://api-jiesuan.yunzhanghu.com/authentication/verify-bankcard-three-factor";
        /// <summary>
        /// 查询账户余额信息
        /// </summary>
        private const string _queryAccountsUrl = "https://api-jiesuan.yunzhanghu.com/api/payment/v1/query-accounts";
        /// <summary>
        /// 获取用户签约状态
        /// </summary>
        private const string _userSignStatusUrl = "https://api-jiesuan.yunzhanghu.com/api/payment/v1/sign/user/status";
        /// <summary>
        /// 用户签约信息上传
        /// </summary>
        private const string _signUserUrl = "https://api-jiesuan.yunzhanghu.com";
        /// <summary>
        /// 实名校验
        /// </summary>
        private const string _verifyIdUrl = "https://api-jiesuan.yunzhanghu.com/authentication/verify-id";
        /// <summary>
        /// 查询某笔订单
        /// </summary>
        private const string _query_realtime_order = "https://api-jiesuan.yunzhanghu.com/api/payment/v1/query-realtime-order";
        #endregion
        private const string _appKey = "O97BnAQ68SAin8Ldr1IwbWczmT01jX3f";
        private const string _tripleDESKey = "qdHmyU74cTudHym2q7Cy2t9U";
        private const string _dealer_id = "00244818";
        private const string _broker_id = "yiyun73";

        // 仓储
        private readonly IRepository<EnterpirsePaymentRecord, Guid> _enterpirsePaymentRecordRepository;
        private readonly IRepository<MqAgent> _mqRepository;

        // config
        private readonly CloudPayConfig _aliPayConfig;

        public YunZhangHuApplicationService(
             IRepository<EnterpirsePaymentRecord, Guid> enterpirsePaymentRecordRepository,
              IRepository<MqAgent> mqRepository,
              IOptions<CloudPayConfig> aliPayConfig
            )
        {
            _enterpirsePaymentRecordRepository = enterpirsePaymentRecordRepository;
            _mqRepository = mqRepository;
            _aliPayConfig = aliPayConfig.Value;
        }
        /// <summary>
        /// 下单
        ///  </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task<OrderPayOutput> PostOrderAsync(PostOrderAsyncInput input)
        {
            var output = new OrderPayOutput();
            var apply = await _enterpirsePaymentRecordRepository.GetAll().Include(s => s.MqAgent).FirstOrDefaultAsync(s => s.Id == input.ApplyId);
            if (apply == null)
            {
                throw new UserFriendlyException($"提现申请不存在！");
            }
            var agent = apply?.MqAgent;
            if (agent == null)
            {
                throw new UserFriendlyException($"代理不存在！");
            }
            var cardNo = "";
            var payUrl = "";
            var requestPlatform = input.RequestPlatform ?? apply.RequestPlatform;
            if (requestPlatform == WithdrawMoneyType.ThirdPartyPaymentALi)
            {
                cardNo = agent.AliPayCardNO;
                payUrl = _aliPayUrl;
            }
            else if (requestPlatform == WithdrawMoneyType.ThirdPartyPaymentChinaPay)
            {
                cardNo = agent.CardNo;
                payUrl = _unionPayUrl;
            }
            if (string.IsNullOrEmpty(cardNo))
            {
                throw new UserFriendlyException($"提现申请卡号不能为空！");
            }
            if (string.IsNullOrEmpty(agent.IdCardNumber))
            {
                throw new UserFriendlyException($"身份证号不能为空！");
            }
            if (string.IsNullOrEmpty(agent.UserName))
            {
                throw new UserFriendlyException($"代理真实姓名不能为空！");
            }
            // 如果是银行卡支付，进行三要素验证
            VerifyBankcardThreeFactor(agent, cardNo, requestPlatform);
            // TODO：
            // 判断商户余额是否大于提现金额
            VerifyBalance(apply, requestPlatform);
            // 当提现方式为支付宝时，校验实名信息
            VerifyUser(agent, requestPlatform);
            // 判断用户是否签约 // 【暂不做】 如果未签约，提交用户签约信息上传
            VerifySignStatus(agent);

            // 准备参数
            var yunzhanghuAliPayData = new YunZhangHuPayData()
            {
                Broker_ID = _broker_id,
                Card_NO = cardNo,
                Check_Name = "NoCheck",
                Dealer_ID = _dealer_id,
                ID_Card = agent.IdCardNumber,
                Notes = string.Empty,// input.Remark,
                Notify_Url = _aliPayConfig.Notify_Url,
                Order_ID = apply.Id.ToString(),
                Pay = apply.Amount.ToString(),
                Pay_Remark = "爸爸在哪儿代理提现",
                Real_Name = agent.UserName
            };
            IRestResponse response = SendRequest(payUrl, yunzhanghuAliPayData);
            // 交订单后的逻辑处理
            // 修改提现申请状态为挂单中
            var orderAliPayOutput = JsonConvert.DeserializeObject<OrderAliPayResponseOutput>(response.Content);
            if (orderAliPayOutput.Code == "0000")
            {
                apply.State = WithdrawDepositState.HangOrder;
                apply.PaymentNo = orderAliPayOutput.Data.Ref;
                apply.WithdrawMoneyType = requestPlatform;
            }
            apply.RequestData = response.Content;
            output.Result = response.Content;
            return output;
        }
        /// <summary>
        /// 查询某个订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  Task<QueryRealtimeOrderOutput> QueryRealtimeOrder(QueryRealtimeOrderInput input)
        {
            var response = new QueryRealtimeOrderOutput();
            

            return null;
        }

        private void VerifyUser(MqAgent agent, WithdrawMoneyType requestPlatform)
        {
            if (requestPlatform == WithdrawMoneyType.ThirdPartyPaymentALi)
            {
                var verifyIdData = new
                {
                    id_card = agent.IdCardNumber,
                    real_name = agent.UserName,
                };
                var verifyIdResponse = SendRequest(_verifyIdUrl, verifyIdData);
                var verifyIdResponseObj = JsonConvert.DeserializeObject<dynamic>(verifyIdResponse.Content);
                if (verifyIdResponseObj.code != "0000")
                {
                    throw new UserFriendlyException("身份证实名验证未通过！");
                }
            }
        }

        /// <summary>
        /// 判断是否签约
        /// </summary>
        /// <param name="agent"></param>
        private void VerifySignStatus(MqAgent agent)
        {
            var userSignStatusData = new
            {
                dealer_id = _dealer_id,
                broker_id = _broker_id,
                real_name = agent.UserName,
                id_card = agent.IdCardNumber
            };
            var getUserSignStatusResponse = SendRequest(_userSignStatusUrl, userSignStatusData, Method.GET);
            var getUserSignStatusResponseObj = JsonConvert.DeserializeObject<dynamic>(getUserSignStatusResponse.Content);
            if (getUserSignStatusResponseObj.code == "0000" && getUserSignStatusResponseObj.data.status != "1")
            {
                var status = $"用户签约状态存在问题！status:{getUserSignStatusResponseObj.data.status}";
                switch (getUserSignStatusResponseObj.data.status.ToString())
                {
                    case "-1":
                        status = "代理不存在签约关系！";
                        break;
                    case "0":
                        status = "代理未签约！";
                        break;
                    case "2":
                        status = "代理已解约！";
                        break;
                }
                throw new UserFriendlyException(status);
            }
        }

        /// <summary>
        /// 判断余额是否足够
        /// </summary>
        /// <param name="apply"></param>
        /// <param name="requestPlatform"></param>
        private void VerifyBalance(EnterpirsePaymentRecord apply, WithdrawMoneyType requestPlatform)
        {
            var queryAccountData = new
            {
                dealer_id = _dealer_id
            };
            var queryAccountResponse = SendRequest(_queryAccountsUrl, queryAccountData, Method.GET);
            Console.WriteLine($"queryAccountResponse=>{queryAccountResponse.Content}");
            var queryAccountResponseObj = JsonConvert.DeserializeObject<dynamic>(queryAccountResponse.Content);
            if (queryAccountResponseObj.code != "0000")
            {
                throw new UserFriendlyException("商户信息查询出错！");
            }
            if (requestPlatform == WithdrawMoneyType.ThirdPartyPaymentChinaPay && Convert.ToDouble(queryAccountResponseObj.data.dealer_infos[0].bank_card_balance) < apply.Amount)
            {
                throw new UserFriendlyException("商户银行卡余额不足！");
            }
            else if (requestPlatform == WithdrawMoneyType.ThirdPartyPaymentALi && Convert.ToDouble(queryAccountResponseObj.data.dealer_infos[0].alipay_balance) < apply.Amount)
            {
                throw new UserFriendlyException("商户支付宝余额不足！");
            }
        }
        /// <summary>
        /// 校验银行卡三元素
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="cardNo"></param>
        /// <param name="requestPlatform"></param>
        private void VerifyBankcardThreeFactor(MqAgent agent, string cardNo, WithdrawMoneyType requestPlatform)
        {
            if (requestPlatform == WithdrawMoneyType.ThirdPartyPaymentChinaPay)
            {
                var verifyBankcardThreeFactor = new
                {
                    Card_No = cardNo,
                    Id_Card = agent.IdCardNumber,
                    Real_Name = agent.UserName
                };
                var verifyBankcardThreeFactorResponse = SendRequest(_verifyBankcardThreeFactorUrl, verifyBankcardThreeFactor);
                var verifyBankcardThreeFactorResponseObj = JsonConvert.DeserializeObject<dynamic>(verifyBankcardThreeFactorResponse.Content);
                if (verifyBankcardThreeFactorResponseObj.code != "0000")
                {
                    throw new UserFriendlyException("银行卡号和用户身份信息未通过验证！");
                }
            }
        }

        /// <summary>
        /// 请求云服务平台接口
        /// </summary>
        /// <param name="payUrl"></param>
        /// <param name="yunzhanghuAliPayData"></param>
        /// <returns></returns>
        private IRestResponse SendRequest(string payUrl, object yunzhanghuAliPayData, Method requestType = Method.POST)
        {
            // 3DES加密 
            var data = TripleDESCrypt.Encrypt(yunzhanghuAliPayData.ToJson().ToLower(), _tripleDESKey);
            var signOutput = GetSign(data);
            var requestParam = $"data={HttpUtility.UrlEncode(data)}&mess={signOutput.Mess}&timestamp={signOutput.TimeStamp}&sign={signOutput.Sign}&sign_type=sha256";
            // 发起请求
            Logger.Debug($"云服务平台接口调用，data参数：{yunzhanghuAliPayData.ToJson()}");
            if (requestType == Method.GET)
            {
                payUrl = $"{payUrl}?{requestParam}";
            }
            var client = new RestClient(payUrl);
            var request = new RestRequest(requestType);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("request-id", DateTime.Now.ToString("yyyyMMddhhmmssffff"));
            request.AddHeader("dealer-id", _dealer_id);
            if (requestType != Method.GET)
            {
                request.AddParameter("undefined", requestParam, ParameterType.RequestBody);
            }
            var response = client.Execute(request);
            return response;
        }

        /// <summary>
        /// 获取日订单（当天的需要第二天查询）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public QueryYunZhangHuOrderOutput GetOrders(QueryYunZhangHuOrderInput input)
        {
            var output = new QueryYunZhangHuOrderOutput();
            // data
            var questParam = new
            {
                Order_Date = input.OrderDate
            };
            var data = TripleDESCrypt.Encrypt(questParam.ToJson().ToLower(), _tripleDESKey);
            var signOutput = GetSign(data);
            var requestParam = $"data={HttpUtility.UrlEncode(data)}&mess={signOutput.Mess}&timestamp={signOutput.TimeStamp}&sign={signOutput.Sign}&sign_type=sha256";
            // 发起请求
            var client = new RestClient(_queryDayOrderUrl + "?" + requestParam);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("request-id", DateTime.Now.ToString("yyyyMMddhhmmssffff"));
            request.AddHeader("dealer-id", _dealer_id);
            var response = client.Execute(request);
            return output;
        }
        /// <summary>
        /// 获取签名
        /// 签名排列顺序为：data=xxx&mess=xxx&timestamp=xxx&key=appkey
        /// </summary>
        /// <param name="strIn">data</param>
        /// <returns></returns>
        private GetYunAccountSignOutput GetSign(string strIn)
        {
            //拼接
            var rand = new Random();
            var ts = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
            var mess = rand.Next(10000, 10000000);
            var signStr = $"data={strIn}&mess={mess}&timestamp={ts}&key={_appKey}";

            var sign = SHA256Crypt.HmacSHA256OutHex(signStr, _appKey);
            return new GetYunAccountSignOutput()
            {
                Mess = mess,
                Sign = sign,
                TimeStamp = ts,
                Data = signStr
            };
        }
        /// <summary>
        /// 更新提现申请结果（回调）
        /// </summary>
        /// <param name="input"></param>
        /// <param name="applyId">申请编号</param>
        /// <returns></returns>
        public async Task<PostUpdateWithdrawApplyRecordOutput> PostUpdateWithdrawApplyRecordAsync(string input)
        {
            //3DES解密
            Logger.Debug("第三方支付回调");
            var data = TripleDESCrypt.Decrypt(input, _tripleDESKey);
            var response = new PostUpdateWithdrawApplyRecordOutput();
            var inputObject = JsonConvert.DeserializeObject<PostUpdateWithdrawApplyRecordInput>(data.Trim());
            var entity = await _enterpirsePaymentRecordRepository.GetAll().Include(s => s.MqAgent).FirstOrDefaultAsync(s => s.Id == inputObject.Data.Order_ID);
            entity.CheckNull("订单不存在！");
            entity.CheckCondition(entity.State == WithdrawDepositState.HangOrder, "订单已完成！");
            var agent = entity?.MqAgent;
            agent.CheckNull("代理不存在！");
            entity.CheckCondition(agent.LockedBalance >= entity.Amount, $"锁定的金额小于提现申请的金额！agentID is:{agent.Id},entity is {entity.ToJson()}");
            entity.PaymentData = input;
            if (inputObject.Data.Status == CloudServiceOrderStatusCodeEnum.Success)
            {
                entity.State = WithdrawDepositState.Success;
                entity.PaymentTime = DateTime.Now;
                agent.LockedBalance -= entity.Amount;
            }
            else if (inputObject.Data.Status == CloudServiceOrderStatusCodeEnum.Fail || inputObject.Data.Status == CloudServiceOrderStatusCodeEnum.FailAndRefunded || inputObject.Data.Status == CloudServiceOrderStatusCodeEnum.Cancle)
            {
                entity.State = WithdrawDepositState.ThirdPartyFail;
                agent.LockedBalance -= entity.Amount;
                agent.Balance += entity.Amount;
            }
            response.IsSuccess = inputObject.Data.Status == CloudServiceOrderStatusCodeEnum.Success;
            response.ErrorMsg = inputObject.Data.Status_Detail_Message;
            return response;
        }

        public async Task<PostSetOrderFailAsyncOutput> PostSetOrderFailAsync(PostSetOrderFailAsyncInput input)
        {
            var response = new PostSetOrderFailAsyncOutput();
            var entity = await _enterpirsePaymentRecordRepository.GetAll().Include(s => s.MqAgent).FirstOrDefaultAsync(s => s.Id == input.OrderId);
            var agent = entity?.MqAgent;
            entity.CheckNull($"订单不存在！{input.OrderId}");
            agent.CheckNull("代理不存在！");
            if (entity.State != WithdrawDepositState.HangOrder)
            {
                response.Msg = "只允许修改挂单状态的提现申请！";
            }
            else
            {
                entity.State = WithdrawDepositState.ThirdPartyFail;
                agent.LockedBalance -= entity.Amount;
                agent.Balance += entity.Amount;
            }
            return response;
        }
    }

}
