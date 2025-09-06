using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.GamblingModelStatistics.Dtos
{
    public class GetErrQuestionDistributionOutput
    {
        public int TaskType { get; set; }

        public int RelationCount { get; set; }

        public int AverageAnswerErrQuestionCount { get; set; }

        public int HighestAnswerErrQuestionCount { get; set; }
    }
}
