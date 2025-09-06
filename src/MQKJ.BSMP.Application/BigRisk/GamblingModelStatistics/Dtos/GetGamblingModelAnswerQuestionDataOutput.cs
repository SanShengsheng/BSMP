using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.GamblingModelStatistics.Dtos
{
    public class GetGamblingModelAnswerQuestionDataOutput
    {
        public int Count { get; set; }

        public int ContinueCount { get; set; }

        public int ThreeQuestionCount { get; set; }

        public int FiveQuestionCount { get; set; }

        public int TenQuestionCount { get; set; }

        public int NormaleCount { get; set; }

        public int WifeCount { get; set; }

        public int ShadyCount { get; set; }

        public int LoversCount { get; set; }

        public int OnlineCount { get; set; }

        public int OfflineCount { get; set; }

        public DateTime DateTime { get; set; }
    }
}
