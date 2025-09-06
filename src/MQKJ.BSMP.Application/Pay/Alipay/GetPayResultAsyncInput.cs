using System;

namespace MQKJ.BSMP.Alipay
{
    public class GetPayResultAsyncInput
    {
        public int FamilyId { get;  set; }
        public Guid OrderId { get;  set; }
        public int GoldCoin { get; set; }
    }
}