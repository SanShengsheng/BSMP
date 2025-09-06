using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos
{
    public class SupplementCoinForFamilyInput
    {
        public string OrderNumber { get; set; }

        public int CoinCount { get; set; }

        public int FamilyId { get; set; }
    }
}
