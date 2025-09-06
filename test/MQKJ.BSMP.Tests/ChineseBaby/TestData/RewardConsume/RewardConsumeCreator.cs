using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MQKJ.BSMP.Tests.Seed
{
    public class RewardConsumeCreator
    {

        private readonly BSMPDbContext _context;

        public RewardConsumeCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateRewardConsume();
        }

        private async void CreateRewardConsume()
        {
            if (_context.Rewards.Any())
            {
                return;
            }
            //TODO
            var rewardAndConsume = new List<Reward>();
            //奖励
            var reward = new Reward()
            {
                Charm = 3,
                CoinCount = 10,
                EmotionQuotient = 15,
                Energy = 11,
                Healthy = 2,
                Imagine = 6,
                Intelligence = 20,
                Name = "奖励test",
                Physique = 13,
                Type = RewardType.Reward,
                WillPower = 8
            };
            rewardAndConsume.Add(reward);
            var reward2 = new Reward
            {
                Name = "default",
                Charm = 0,
                EmotionQuotient = 0,
                Energy = 0,
                Healthy = 0,
                Imagine = 0,
                Intelligence = 0,
                Physique = 0,
                WillPower = 0,
                Type = RewardType.Reward
            };
            rewardAndConsume.Add(reward2);

            //奖励
            var consume = new Reward()
            {
                Charm = 1,
                CoinCount = 2,
                EmotionQuotient = 1,
                Energy = 5,
                Healthy = 2,
                Imagine = 0,
                Intelligence = 9,
                Name = "惩罚-test",
                Physique = 11,
                Type = RewardType.Consume,
                WillPower = 10
            };
            rewardAndConsume.Add(consume);
            await _context.Rewards.AddRangeAsync(rewardAndConsume);
        }
    }
}
