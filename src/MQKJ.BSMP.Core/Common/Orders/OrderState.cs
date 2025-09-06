namespace MQKJ.BSMP.Orders
{
    public enum OrderState
    {
        /// <summary>
        /// 未知
        /// </summary>
        [EnumHelper.EnumDescription("未知")]
        UnKnow = 0,

        /// <summary>
        /// 未付款
        /// </summary>
        [EnumHelper.EnumDescription("未付款")]
        UnPaid = 1,

        /// <summary>
        /// 已付款
        /// </summary>
        [EnumHelper.EnumDescription("已付款")]
        Paid = 2,

        /// <summary>
        /// 待发货
        /// </summary>
        //Shipped = 3,

        /// <summary>
        /// 已发货
        /// </summary>
        //dispatched = 4,

        /// <summary>
        /// 已收货(已签收)
        /// </summary>
        //ReceivedGood = 5,

        /// <summary>
        /// 退款申请
        /// </summary>
        //RefundApplication = 6,

        /// <summary>
        /// 退款中
        /// </summary>
        //Refunding = 7,

        /// <summary>
        /// 已退款
        /// </summary>
        //Refunded = 8,

        /// <summary>
        /// 取消交易
        /// </summary>
        //CancelTransaction = 9
        [EnumHelper.EnumDescription("付款失败")]
        Failed = 99
    }
}