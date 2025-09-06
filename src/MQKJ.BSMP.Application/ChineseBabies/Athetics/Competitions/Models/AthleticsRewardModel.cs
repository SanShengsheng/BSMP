using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Models
{
    public class AthleticsRewardModel
    {
        public int AthleticsRewardId { get; set; }

        public int RankingNumber { get; set; }

        public RewardPropType RewardPropType { get; set; }

        public int CoinCount { get; set; }

        public int? BabyPropPriceId { get; set; }
        public int? BabyPropId { get; set; }
    }
}
