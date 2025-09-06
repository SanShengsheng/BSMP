using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MQKJ.BSMP.Tests.Seed
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
            _context.SaveChanges();
            CreateGrowUpOptions();
            CreateStudyOptions();
        }

        private void CreateGrowUpOptions()
        {
            if (!_context.BabyEventOptions.Any())
            {
                var reward = _context.Rewards.FirstOrDefault(s => s.Type == RewardType.Reward);
                var consume = _context.Rewards.FirstOrDefault(s => s.Type == RewardType.Consume);
                var babyEventList = _context.EventGroupBabyEvents.Include(s => s.BabyEvent).Include(s => s.EventGroup);
                var babyEvent = babyEventList.Where(s => s.EventGroup.Description == "2个月");
                var options = new List<BabyEventOption>();
                if (babyEvent == null)
                {
                    System.Console.WriteLine("宝宝选项为null");
                    return;
                }
                foreach (var item in babyEvent)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var option = new BabyEventOption()
                        {
                            Content = "2个月选项" + i + 1,
                            ImagePath = $"growUpOptionTest0{i + 1}.jpg",
                            ConsumeId = consume.Id,
                            RewardId = reward.Id,
                            BabyEventId = item.EventId,
                        };
                        options.Add(option);
                    }
                }
                _context.BabyEventOptions.AddRange(options);
            }

        }
        private void CreateStudyOptions()
        {
            //if (!_context.BabyEventOptions.Any())
            //{
            var reward = _context.Rewards.FirstOrDefault(s => s.Type == RewardType.Reward);
            var consume = _context.Rewards.FirstOrDefault(s => s.Type == RewardType.Consume);
            var babyEvent = _context.EventGroupBabyEvents.Include(s => s.BabyEvent).Include(s => s.EventGroup).Where(s => s.EventGroup.Description == "1个月");
            var options = new List<BabyEventOption>();
            if (babyEvent == null)
            {
                System.Console.WriteLine("宝宝选项为null");
                return;
            }
            foreach (var item in babyEvent)
            {
                for (int i = 0; i < 3; i++)
                {
                    var option = new BabyEventOption()
                    {
                        Content = "1个月选项" + i + 1,
                        ImagePath = $"studyOptionTest0{i + 1}.jpg",
                        ConsumeId = consume.Id,
                        RewardId = reward.Id,
                        BabyEventId = item.EventId,
                    };
                    options.Add(option);
                }
            }

            _context.BabyEventOptions.AddRange(options);
            //}

        }
    }
}
