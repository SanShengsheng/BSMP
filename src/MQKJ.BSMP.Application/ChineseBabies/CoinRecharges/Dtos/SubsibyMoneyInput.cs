using System;

namespace MQKJ.BSMP.ChineseBabies
{
    public class SubsidyMoneyInput
    {
        public int FamilyId { get; set; }

        public double SubsidyMoneyAmount { get; set; }

        public string SMSCode { get; set; }

        public int Draww { get; set; }

        public DateTime SubsidyDate { get; set; }
    }
}