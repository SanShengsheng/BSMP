using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using MQKJ.BSMP.ChineseBabies.Prestiges;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.ChineseBaby.FamilyWorships
{
    public class WorshipRewardsCreator
    {
        private readonly BSMPDbContext _context;

        public WorshipRewardsCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateWorshipRewardsCreator();
        }
        private FamilyWorshipReward CreateInstance(int max, int min, int mincoins, int maxcoins, FamilyWorshipRewardType type, int pre) => 
            new FamilyWorshipReward
            {
                 CoinsMax = maxcoins , CoinsMin = mincoins , RankMax =max,RankMin =min,  Type = type, Prestiges = pre
            };
        private void CreateWorshipRewardsCreator()
        {
            if (!_context.FamilyWorshipRewards.Any())
            {
                var preinserts = new FamilyWorshipReward[24] {
                    this.CreateInstance(1,1,3000,5000, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(2,2,2700,4500, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(3,3,2430,4050, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(4,4,2187,3645, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(5,5,1968,3280, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(6,  6,  1771,   2952, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(7,  7,  1593,   2656, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(8,  8,  1433,   2390, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(9,  9,  1289,   2151, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(10, 10, 1160,   1935, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(11, 15, 1044,   1741, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(16, 20, 939,    1566, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(21, 30, 845,    1409,FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(31, 40, 760,    1268, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(41, 50, 684,    1141, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(51, 100,    615,    1026, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(101 ,200,   553 ,923, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(201 ,500,   497,    830, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(501,    999999, 447,    747, FamilyWorshipRewardType.COIN,-1),
                    this.CreateInstance(1,5,-1,-1, FamilyWorshipRewardType.Prestige,5),
                    this.CreateInstance(6,10,-1,-1, FamilyWorshipRewardType.Prestige,4),
                    this.CreateInstance(11,20,-1,-1, FamilyWorshipRewardType.Prestige,3),
                    this.CreateInstance(21,100,-1,-1, FamilyWorshipRewardType.Prestige,2),
                    this.CreateInstance(101,9999999,-1,-1, FamilyWorshipRewardType.Prestige,1)
                };
        
                _context.FamilyWorshipRewards.AddRange(preinserts);
            }
            if (!_context.SystemSettings.Any(s => s.Name.Equals("WorshipedTimesMax")))
            {
                _context.SystemSettings.Add(new SystemSetting {
                    Name = "WorshipedTimesMax",
                    Value = "100",
                    Description = "每天被膜拜的最大次数",
                    GroupName = string.Empty,
                });
            }
            if (!_context.SystemSettings.Any(s => s.Name.Equals("ToWorshipTimesMax")))
            {
                _context.SystemSettings.Add(new SystemSetting {
                    Name = "ToWorshipTimesMax",
                    Value = "5",
                    Description ="每天膜拜次数限制",
                    GroupName =string.Empty
                });
            }

        }
    }
}
