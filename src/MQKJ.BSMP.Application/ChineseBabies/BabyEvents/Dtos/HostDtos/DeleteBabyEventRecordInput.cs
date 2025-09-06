using System;

namespace MQKJ.BSMP.ChineseBabies
{
    public class DeleteBabyEventRecordInput
    {
        public int BabyId { get; set; }

        public Guid PlayerGuid { get; set; }
    }
}