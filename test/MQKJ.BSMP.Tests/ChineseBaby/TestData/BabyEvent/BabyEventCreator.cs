using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MQKJ.BSMP.Tests.Seed
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
            //if (!_context.BabyEvents.Any())
            //{
            var reward = _context.Rewards.FirstOrDefault(s => s.Type == RewardType.Reward);
            var consume = _context.Rewards.FirstOrDefault(s => s.Type == RewardType.Consume);
            var babyEvents = new List<BabyEvent>();
            //TODO
            #region 学习
            // 学习
            var babyStudyEvent = new BabyEvent()
            {
                Id = 1,
                Aside = "我是22222旁白君",
                CountDown = 40,
                ImagePath = "a.jpg",
                IsBlock = false,
                Name = "学走路",
                OperationType = OperationType.Single,
                RewardId = reward?.Id,
                ConsumeId = consume?.Id,
                MaxValue = 100,
                MinValue = 30,
                Type = IncidentType.Study,
                ConditionType = ConditionType.Normal,
                StudyAllowMaxTime=1,
            };
            babyEvents.Add(babyStudyEvent);
            //属性触发
            var babyStudyEventByPropertyTrigger = new BabyEvent()
            {
                Id = 2,
                Aside = "我是旁11111111白君",
                CountDown = 60,
                ImagePath = "a.jpg",
                IsBlock = false,
                Name = "摔倒了",
                OperationType = OperationType.Single,
                RewardId = reward?.Id,
                ConsumeId = consume?.Id,
                MaxValue = 100,
                MinValue = 30,
                Type = IncidentType.Study,
                ConditionType = ConditionType.Property,
                BabyProperty = BabyProperty.Healthy,
            };
            babyEvents.Add(babyStudyEventByPropertyTrigger);
            //触发其他事件，改事件将被置为已完成
            var babyStudyEventByEventTrigger = new BabyEvent()
            {
                Id = 3,
                Aside = "我是要触发其他的学习事件",
                CountDown = 60,
                ImagePath = "a.jpg",
                IsBlock = false,
                Name = "摔倒了",
                OperationType = OperationType.Single,
                RewardId = reward?.Id,
                ConsumeId = consume?.Id,
                MaxValue = 100,
                MinValue = 30,
                Type = IncidentType.Study,
                ConditionType = ConditionType.Normal,
                EventId = 4,
            };
            babyEvents.Add(babyStudyEventByEventTrigger);
            //被触发的事件，需要根据触发事件对象是否已完成来判断
            var babyStudySpecialEvent = new BabyEvent()
            {
                Id = 4,
                Aside = "我是被触发的学习事件",
                CountDown = 40,
                ImagePath = "a.jpg",
                IsBlock = false,
                Name = "摔跤了",
                OperationType = OperationType.Single,
                RewardId = reward?.Id,
                ConsumeId = consume?.Id,
                MaxValue = 100,
                MinValue = 30,
                Type = IncidentType.Special,
                ConditionType = ConditionType.Event,
            };
            babyEvents.Add(babyStudySpecialEvent);
            #endregion

            await _context.BabyEvents.AddRangeAsync(babyEvents);
            //}

        }
        private async void CreateBabyGrowUpEvent()
        {
            //if (_context.BabyEvents.Any())
            //{
            var reward = _context.Rewards.FirstOrDefault(s => s.Type == RewardType.Reward);
            var consume = _context.Rewards.FirstOrDefault(s => s.Type == RewardType.Consume);
            var babyEvents = new List<BabyEvent>();
            //TODO
            #region 成长
            var babyGroupEvent = new BabyEvent()
            {
                Id=30,
                Aside = "我是旁白君",
                CountDown = 40,
                ImagePath = "a.jpg",
                IsBlock = false,
                Name = "宝宝哭啦",
                OperationType = OperationType.Single,
                RewardId = reward?.Id,
                ConsumeId = consume?.Id,
                MaxValue = 100,
                MinValue = 30,
                Type = IncidentType.Growup,
                ConditionType = ConditionType.Normal,
            };
            babyEvents.Add(babyGroupEvent);
            // 成长特殊
            var babyGrowUpSpecialEventByPropertyTrigger = new BabyEvent()
            {
                Id=31,
                Aside = "我是旁白君",
                CountDown = 40,
                ImagePath = "a.jpg",
                IsBlock = false,
                Name = "宝宝哭啦",
                OperationType = OperationType.Single,
                RewardId = reward?.Id,
                ConsumeId = consume?.Id,
                MaxValue = 100,
                MinValue = 30,
                Type = IncidentType.Growup,
                ConditionType = ConditionType.Property,
                BabyProperty=BabyProperty.Happiness
            };
            babyEvents.Add(babyGrowUpSpecialEventByPropertyTrigger);
            var babyGroupEventByEventTrigger = new BabyEvent()
            {
                Id = 32,
                Aside = "我是要触发其他的成长事件",
                CountDown = 40,
                ImagePath = "a.jpg",
                IsBlock = false,
                Name = "宝宝哭啦",
                OperationType = OperationType.Single,
                RewardId = reward?.Id,
                ConsumeId = consume?.Id,
                MaxValue = 100,
                MinValue = 30,
                Type = IncidentType.Growup,
                ConditionType = ConditionType.Normal,
                EventId=33
            };
            babyEvents.Add(babyGroupEventByEventTrigger);
            // 成长特殊
            var babyGrowUpSpecialEvent = new BabyEvent()
            {
                Id = 33,
                Aside = "我是旁白君",
                CountDown = 40,
                ImagePath = "a.jpg",
                IsBlock = false,
                Name = "宝宝哭啦",
                OperationType = OperationType.Single,
                RewardId = reward?.Id,
                ConsumeId = consume?.Id,
                MaxValue = 100,
                MinValue = 30,
                Type = IncidentType.Special,
                ConditionType = ConditionType.Event,
            };
            babyEvents.Add(babyGrowUpSpecialEvent);
            //阻断性事件
            var babyBlockGroupEvent = new BabyEvent()
            {
                Id = 34,
                Aside = "我是成长的阻断性事件",
                CountDown = 4,
                ImagePath = "我是成长的阻断性事件.jpg",
                IsBlock = true,
                Name = "发高烧，40度",
                OperationType = OperationType.Single,
                RewardId = reward?.Id,
                ConsumeId = consume?.Id,
                MaxValue = 100,
                MinValue = 30,
                Type = IncidentType.Block,
                ConditionType = ConditionType.Property,
            };
            babyEvents.Add(babyBlockGroupEvent);
            //过期性事件
            var babyExpiredGroupEvent = new BabyEvent()
            {
                Id = 35,
                Aside = "我是成长的过期性事件",
                CountDown = 4,
                ImagePath = "我是成长的过期性事件.jpg",
                IsBlock = false,
                Name = "去海洋馆",
                OperationType = OperationType.Single,
                RewardId = reward?.Id,
                ConsumeId = consume?.Id,
                MaxValue = 100,
                MinValue = 30,
                Type = IncidentType.Special,
                ConditionType = ConditionType.Property,
                 ExpirationGroupId=1,
            };
            babyEvents.Add(babyExpiredGroupEvent);
            #endregion
            await _context.BabyEvents.AddRangeAsync(babyEvents);
            //}

        }
    }
}
