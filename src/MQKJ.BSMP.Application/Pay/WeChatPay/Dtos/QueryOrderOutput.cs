using MQKJ.BSMP.Orders;

namespace MQKJ.BSMP.WeChatPay.Dtos
{
    public class QueryOrderOutput
    {
        public bool OrderPayResult { get; set; }
        public string OrderNumber { get; set; }
        public double PayAmount { get; set; }

        /// <summary>
        /// 是否已经更新过订单状态
        /// </summary>
        public bool IsUpdateOrder { get; set; }

        public BussinessState BussinessState { get; set; }
    }
}