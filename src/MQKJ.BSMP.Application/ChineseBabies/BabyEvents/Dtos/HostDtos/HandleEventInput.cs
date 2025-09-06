using System;

namespace MQKJ.BSMP.ChineseBabies
{
    public class HandleEventInput
    {
        public int BabyId { get; set; }

        public int EventId { get; set; }

        public Guid PlayerGuid { get; set; }

        public int OptionId { get; set; }

        public Guid TheOtherGuid { get; set; }

        public int FamilyId { get; set; }

    }
}