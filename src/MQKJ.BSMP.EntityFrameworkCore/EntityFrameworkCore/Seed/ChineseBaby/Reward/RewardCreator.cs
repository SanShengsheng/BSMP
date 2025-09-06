using MQKJ.BSMP.ChineseBabies;
using System.Collections.Generic;
using System.Linq;
namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class RewardCreator
    {

        private readonly BSMPDbContext _context;

        public RewardCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateReward();
        }

        private void CreateReward()
        {
            if (_context.Rewards.Any())
            {
                return;
            }
            var reward = new Reward();
            var rewards = _context.Rewards.Count();
            if (rewards == 0)
            {

                 reward = new Reward
                {
                    Name = "default",
                    Charm = 10,
                    EmotionQuotient = 10,
                    Energy = 10,
                    Healthy = 10,
                    Imagine = 10,
                    Intelligence = 10,
                    Physique = 10,
                    WillPower = 10,
                    Type = RewardType.Reward
                };
                _context.Rewards.Add(reward);
                var consume = new Reward
                {
                    Name = "default",
                    Charm = -10,
                    EmotionQuotient = -1,
                    Energy = 0,
                    Healthy = -2,
                    Imagine = 0,
                    Intelligence = 0,
                    Physique = -1,
                    WillPower = -20,
                    Type = RewardType.Consume
                };
                _context.Rewards.Add(consume);
            }
        }
    }
}
