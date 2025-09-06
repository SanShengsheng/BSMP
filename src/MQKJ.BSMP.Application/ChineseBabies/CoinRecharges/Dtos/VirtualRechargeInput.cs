using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos
{
    public class VirtualRechargeInput
    {
        public int Id { get; set; }

        public int FamilyId { get; set; }


        public Guid PlayerId { get; set; }
    }
}
