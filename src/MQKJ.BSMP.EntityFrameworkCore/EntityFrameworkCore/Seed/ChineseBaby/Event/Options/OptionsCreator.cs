using MQKJ.BSMP.ChineseBabies;
using System.Collections.Generic;
using System.Linq;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class OptionsCreator
    {

        private readonly BSMPDbContext _context;

        public OptionsCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            //_context.SaveChanges();
            //CreateGrowUpOptions();
            //CreateStudyOptions();
        }

        private void CreateGrowUpOptions()
        {
            if (!_context.BabyEventOptions.Any(s => s.Content.Contains("成长选项")))
            {
                var reward = _context.Rewards.FirstOrDefault(s => s.Type == RewardType.Reward);
                var consume = _context.Rewards.FirstOrDefault(s => s.Type == RewardType.Consume);
                var eventGroup = _context.EventGroups.FirstOrDefault(s => s.Description == "成长");
                var eventGroupBabyEvents = _context.EventGroupBabyEvents.FirstOrDefault(s => s.GroupId == eventGroup.Id);
                var options = new List<BabyEventOption>();
                if (eventGroupBabyEvents != null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var option = new BabyEventOption()
                        {
                            Content = "成长选项" + i + 1,
                            ImagePath = $"growUpOptionTest0{i + 1}.jpg",
                            ConsumeId = consume?.Id,
                            RewardId = reward?.Id,
                            BabyEventId = eventGroupBabyEvents.EventId,
                        };
                        options.Add(option);
                    }
                    _context.BabyEventOptions.AddRange(options);
                }
            }
        }
        private void CreateStudyOptions()
        {
            if (!_context.BabyEventOptions.Any(s=>s.Content.Contains("学习选项")))
            {
                var reward = _context.Rewards.FirstOrDefault(s => s.Type == RewardType.Reward);
                var consume = _context.Rewards.FirstOrDefault(s => s.Type == RewardType.Consume);
                var eventGroup = _context.EventGroups.FirstOrDefault(s => s.Description == "学习");
                var eventGroupBabyEvents = _context.EventGroupBabyEvents.FirstOrDefault(s => s.GroupId == eventGroup.Id);
                if (eventGroupBabyEvents != null)
                {
                    var options = new List<BabyEventOption>();
                    for (int i = 0; i < 3; i++)
                    {
                        var option = new BabyEventOption()
                        {
                            Content = "学习选项" + i + 1,
                            ImagePath = $"studyOptionTest0{i + 1}.jpg",
                            ConsumeId = consume?.Id,
                            RewardId = reward?.Id,
                            BabyEventId = eventGroupBabyEvents.EventId,
                        };
                        options.Add(option);
                    }
                    _context.BabyEventOptions.AddRange(options);
                }

            }

        }
    }
}
