using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.PassModelStatistics.Dtos
{
    public class GetPassModelAnswerQuestionDataOutput
    {
        public int TotalCount { get; set; }

        public int RightCount { get; set; }

        public int RealCount { get; set; }

        public int CheatCount { get; set; }

        public DateTime DateTime { get; set; }
    }
}
