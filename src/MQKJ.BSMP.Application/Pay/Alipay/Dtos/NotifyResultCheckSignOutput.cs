using MQKJ.BSMP.Orders;
using MQKJ.BSMP.Pay.Alipay.Dtos;

namespace MQKJ.BSMP.Alipay
{
    public class NotifyResultCheckSignOutput
    {
        public bool Status { get; internal set; }
        public Order Order { get; internal set; }
        public AliPayNotifyRsultAsyncDto Notify { get; internal set; }
    }
}