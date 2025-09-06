using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.ChineseBaby.Athletics.AthleticsRewards
{
    public class AthleticsRewardCreator
    {
        private readonly BSMPDbContext _context;

        public AthleticsRewardCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateAthleticsReward();
        }

        private void CreateAthleticsReward()
        {
            if (!_context.AthleticsRewards.Any(c => c.Code == 1))
            {
                var athleticsReward = new AthleticsReward();
                athleticsReward.Code = 1;
                athleticsReward.RewardPropType = RewardPropType.CoinType;
                athleticsReward.RankingNumber = 1;
                athleticsReward.CoinCount = 100;
                _context.AthleticsRewards.Add(athleticsReward);
            }
            if (!_context.AthleticsRewards.Any(c => c.Code == 2))
            {
                var athleticsReward = new AthleticsReward();
                athleticsReward.Code = 1;
                athleticsReward.RewardPropType = RewardPropType.CoinType;
                athleticsReward.RankingNumber = 1;
                athleticsReward.BabyPropId = _context.BabyProps.FirstOrDefault(c => true).Id;
                athleticsReward.BabyPropPriceId = _context.BabyPropPrices.FirstOrDefault(c => true).Id;
                _context.AthleticsRewards.Add(athleticsReward);
            }
        }
    }
}
