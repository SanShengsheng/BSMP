using System;

namespace MQKJ.BSMP.Orders.Dtos
{
    public class WechatPayUpdateOrderStateInput
    {
        public WechatPayUpdateOrderStateInput(bool isSuccess, int totalFee, int realTotalFee, string paymentData)
        {
            IsSuccess = isSuccess;
            PayAmount = Math.Round(totalFee / 100f, 2);
            PaymentData = paymentData;
            RealAmount = Math.Round(realTotalFee / 100f, 2);
        }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 微信订单号
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 支付完成时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 支付结果 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public double PayAmount { get; set; }

        /// <summary>
        /// 实际获取金额
        /// </summary>
        public double RealAmount { get; set; }

        /// <summary>
        /// 接口返回数据
        /// </summary>
        public string PaymentData { get; set; }
    }
}