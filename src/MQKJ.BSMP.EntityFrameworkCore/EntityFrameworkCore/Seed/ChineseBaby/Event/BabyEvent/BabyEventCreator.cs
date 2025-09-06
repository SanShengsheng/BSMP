using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class BabyEventCreator
    {

        private readonly BSMPDbContext _context;

        public BabyEventCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateBabyStudyEvent();
            CreateBabyGrowUpEvent();
        }

        private async void CreateBabyStudyEvent()
        {
            if (!_context.BabyEvents.Any(b => b.Type == IncidentType.Study))
            {
                var rewards = _context.Rewards.Where(s => s.Type == RewardType.Reward).ToList();
                var consumes = _context.Rewards.Where(s => s.Type == RewardType.Consume).ToList();
                var count = consumes.Count==0?1:consumes.Count;
                var rcount = rewards.Count==0?1:rewards.Count;
                var i = 0;
                foreach (var reward in rewards)
                {
                    // 学习
                    var babyStudyEvent = new BabyEvent()
                    {
                        Aside = $"我是旁白君100{i}",
                        CountDown = 40,
                        ImagePath = "a.jpg",
                        IsBlock = false,
                        Name = $"学走路2001{i}",
                        OperationType = OperationType.Single,
                        //RewardId = reward?.Id,
                        //ConsumeId = consumes[i % count].Id,
                        Type = IncidentType.Study,
                        ConditionType = ConditionType.Normal,
                        Description = $"学走路2001{i}的描述",
                        Options = new List<BabyEventOption>
                            {
                                new BabyEventOption
                                {
                                   Content = "选项一",
                                   ConsumeId = consumes[i % count].Id,
                                   Sort = 0,
                                   ImagePath = "options.jpg",
                                   RewardId = rewards[i % rcount].Id,
                                },
                                new BabyEventOption
                                {
                                   Content = "选项二",
                                   ConsumeId = consumes[i % count].Id,
                                   Sort = 1,
                                   ImagePath = "options.jpg",
                                   RewardId = rewards[i % rcount].Id,
                                },
                                new BabyEventOption
                                {
                                   Content = "选项三",
                                   ConsumeId = consumes[i % count].Id,
                                   Sort = 2,
                                   ImagePath = "options.jpg",
                                   RewardId = rewards[i % rcount].Id,
                                },
                            }
                    };
                    i++;
                    await _context.BabyEvents.AddAsync(babyStudyEvent);
                }
            }

    }
        private  void CreateBabyGrowUpEvent()
        {
            if (!_context.BabyEvents.Any(b => b.Type == IncidentType.Growup))
            {
                var rewards = _context.Rewards.Where(s => s.Type == RewardType.Reward).ToList();
                var consumes = _context.Rewards.Where(s => s.Type == RewardType.Consume).ToList();
                var count = consumes.Count ;
                var rcount = rewards.Count ;
                var i = 0;
                foreach (var reward in rewards)
                {
                    // 学习
                    var babyStudyEvent = new BabyEvent()
                    {
                        Aside = $"我是旁白君300{i},我是很长很长的旁白",
                        CountDown = 40,
                        ImagePath = "a.jpg",
                        IsBlock = false,
                        Name = $"学走路4001{i}",
                        OperationType = i % 3 == 0 ? OperationType.Double : OperationType.Single,
                        //RewardId = reward?.Id,
                        //ConsumeId = consumes[i % count].Id,
                        Type = IncidentType.Growup,
                        ConditionType = ConditionType.Normal,
                        Options = new List<BabyEventOption>
                        {
                            new BabyEventOption
                            {
                               Content = "选项一",
                               ConsumeId =count==0?null: (int?)consumes[i % count].Id,
                               Sort = 0,
                               ImagePath = "options.jpg",
                               RewardId = rewards[i % rcount].Id,
                            },
                            new BabyEventOption
                            {
                               Content = "选项二",
                               ConsumeId = consumes[i % count].Id,
                               Sort = 1,
                               ImagePath = "options.jpg",
                               RewardId = rewards[i % rcount].Id,
                            },
                            new BabyEventOption
                            {
                               Content = "选项三",
                               ConsumeId = consumes[i % count].Id,
                               Sort = 2,
                               ImagePath = "options.jpg",
                               RewardId = rewards[i % rcount].Id,
                            },
                        }
                    };
                    i++;
                    _context.BabyEvents.Add(babyStudyEvent);
                }
            }

        }
    }
}
