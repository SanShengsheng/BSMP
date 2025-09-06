using System;
using MQKJ.BSMP.Orders;

namespace MQKJ.BSMP.Alipay
{
    public class AliPayH5Input
    {
        public decimal Totalfee { get;  set; }
        public int? ProductId { get;  set; }
        public Guid PlayerId { get;  set; }
        public GoodsType GoodsType { get;  set; }
        public int? FamilyId { get;  set; }
        public int ProductAmount { get; internal set; }
    }
}