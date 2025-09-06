using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos
{
    public class UpdateRechargeDataInput
    {
        public int FamilyId { get; set; }

        public int ProductId { get; set; }

        public Guid PlayerId { get; set; }

        public Guid OrderId { get; set; }
    }
}
