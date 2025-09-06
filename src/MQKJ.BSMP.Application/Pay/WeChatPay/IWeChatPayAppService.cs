using Abp.Application.Services;
using Microsoft.AspNetCore.Http;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using MQKJ.BSMP.WeChatPay.Dtos;
using MQKJ.BSMP.WeChatPay.Models;
using System.Threading.Tasks;

namespace MQKJ.BSMP.WeChatPay
{
    public interface IWeChatPayAppService : IApplicationService
    {
        Task GetId();

        /// <summary>
        /// 发起支付请求-旧接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SendPaymentRquestOutput> SendPaymentRquest(SendPaymentRquestInput input);

        /// <summary>
        /// 发起支付-新街口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<MiniProgramPayOutput> Pay(SendPaymentRquestInput input);

        /// <summary>
        /// 支付通知回调-旧接口
        /// </summary>
        /// <returns></returns>
        //Task<string> getNotifyInfo();

        /// <summary>
        /// 支付通知回调-新接口
        /// </summary>
        /// <returns></returns>
        Task<string> GetPayNotify();

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<string> QueryOrderState(QueryOrderStateInput input);

        /// <summary>
        /// 查询订单-新接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<QueryOrderOutput> QueryOrder(QueryOrderStateInput input);

        Task<MiniProgramPayOutput> H5Pay(SendPaymentRquestInput input);

        /// <summary>
        /// 查询微信订单状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OrderQueryOutput QueryWechatPayResult(QueryOrderStateInput input);

        Task<WechatNotifyResponse> WechatPayNotify();
    }
}