using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.TestModels
{
    public class TestFightResultInput
    {
        public BabyAttributeCode AttributeCode { get; set; } 

        public int SelfBabyId { get; set; }

        public int OtherBabyId { get; set; }
    }
}
