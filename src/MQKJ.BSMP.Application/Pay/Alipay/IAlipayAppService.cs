using Abp.Application.Services;
using MQKJ.BSMP.Pay.Alipay.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Alipay
{
    public interface IAlipayAppService: IApplicationService
    {
        /// <summary>
        /// 支付宝支付
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> Pay(AliPayH5Input input);
        /// <summary>
        /// 支付宝异步返回结果的验证签名
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
         Task<NotifyResultCheckSignOutput> NotifyResultCheckSign(AliPayNotifyRsultAsyncDto requestParam);
        /// <summary>
        /// 获取充值结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AliPayResultOutput> GetPayResultAsync(GetPayResultAsyncInput input);
    }
}
