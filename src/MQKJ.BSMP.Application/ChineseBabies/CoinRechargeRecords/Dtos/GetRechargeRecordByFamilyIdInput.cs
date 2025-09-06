using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.CoinRechargeRecords.Dtos
{
    public class GetRechargeRecordByFamilyIdInput
    {
        public int FamilyId { get; set; }

        public RechargeLevel RechargeLevel { get; set; }
    }
}
