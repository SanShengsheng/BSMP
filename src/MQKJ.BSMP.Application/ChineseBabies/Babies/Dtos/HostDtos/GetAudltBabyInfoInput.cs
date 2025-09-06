using System;

namespace MQKJ.BSMP.ChineseBabies
{
    public class GetAudltBabyInfoInput
    {
        public int BabyId { get; set; }

        public  Guid PlayerGuid { get; set; }
    }
}