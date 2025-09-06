namespace MQKJ.BSMP.AliPay
{
    internal class OrderAliPayResponseOutput
    {
        public OrderAliPayResponseOutput()
        {
        }

        public string Code { get; set; }

        public OrderAliPayResponseOutputData Data { get; set; }

        public string Message { get; set; }

        public string  Request_ID { get; set; }
    }

    public class OrderAliPayResponseOutputData
    {
        public string Pay { get; set; }
        /// <summary>
        /// 综合服务平台流水
        /// </summary>
        public string Ref { get; set; }

        public string Order_ID { get; set; }

    }
}