using Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.AliPay;
using MQKJ.BSMP.Orders.Dtos;
using System;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Pay
{
    /// <summary>
    /// 支付宝支付
    /// </summary>
    public interface IYunZhangHuApplicationService : IApplicationService
    {
        /// <summary>
        /// 下订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrderPayOutput> PostOrderAsync(PostOrderAsyncInput input);
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        QueryYunZhangHuOrderOutput GetOrders(QueryYunZhangHuOrderInput input);
        /// <summary>
        /// 更新提现申请记录（云账户申请回调）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PostUpdateWithdrawApplyRecordOutput> PostUpdateWithdrawApplyRecordAsync(string input);
        /// <summary>
        /// 将订单标记为失败
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PostSetOrderFailAsyncOutput> PostSetOrderFailAsync(PostSetOrderFailAsyncInput input);
        /// <summary>
        /// 查询某笔订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<QueryRealtimeOrderOutput> QueryRealtimeOrder(QueryRealtimeOrderInput input);


    }
}
