using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos
{
    public class SupplementCoinRechargeInput
    {
        public int? FamilyId { get; set; }

        public int CoinCount { get; set; }

        public string OrdeNum { get; set; }
    }
}
