using System;

namespace MQKJ.BSMP.ChineseBabies
{
    public class GetFamilyStateInput
    {
        public Guid FatherId { get; set; }

        public Guid MotherId { get; set; }
    }
}