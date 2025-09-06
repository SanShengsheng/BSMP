using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using MQKJ.BSMP.ChineseBabies;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MQKJ.BSMP.Tests.Events
{
    public class BabyEventAppService_Tests : BSMPTestBase
    {
        private readonly IBabyEventAppService _entityAppService;
        private readonly IDistributedCache _memoryCache;

        public BabyEventAppService_Tests()
        {
            _entityAppService = Resolve<IBabyEventAppService>();
            _memoryCache = Resolve<IDistributedCache>();
        }
        /// <summary>
        /// 应该获取成长事件
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Get_GrowUpEvents()
        {
            // Act

            var babies = await GetAllBabies();
            var baby = babies.FirstOrDefault(s => s.GroupId == 2);
            var output = await _entityAppService.GetGrowUpEvents(new GetGrowUpEventsInput
            {
                BabyId = baby.Id,
                PlayerGuid = baby.Family.FatherId
            });
            //// Assert
            output.ShouldNotBeNull();
            output.GrowUpEvents.ShouldNotBeNull();
            output.GrowUpEmergencies.ShouldNotBeNull();
        }
        /// <summary>
        /// 应该获取学习事件
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Get_StudyEvents()
        {
            // Act

            var babies = await GetAllBabies();
            var baby = babies.FirstOrDefault(s => s.GroupId == 1);
            var output = await _entityAppService.GetStudyEvents(new GetStudyEventsInput
            {
                BabyId = baby.Id,
                PlayerGuid = baby.Family.FatherId
            });
            //// Assert
            output.ShouldNotBeNull();
            output.StudyEvents.ShouldNotBeNull();
            output.StudyEmergencies.ShouldNotBeNull();
        }

        /// <summary>
        /// 应该获取学习突发事件通过属性触发
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Get_Emergencies_StudyEvent_By_Property_Trigger()
        {
            var babies = await GetAllBabies();
            var baby = babies.FirstOrDefault();
            var targetEventId = 0;
            await UsingDbContextAsync(async context =>
            {
                var babyItem = await context.Babies.FindAsync(baby.Id);
                var babyEvent = await context.EventGroupBabyEvents.Include(s => s.BabyEvent).Where(s => s.BabyEvent.Type == IncidentType.Study && s.GroupId == baby.GroupId && s.BabyEvent.ConditionType == ConditionType.Property).FirstOrDefaultAsync();
                babyItem.Healthy = Convert.ToInt32(babyEvent.BabyEvent.MaxValue) + 1;
                context.Entry(babyItem).State = EntityState.Modified;
                await context.SaveChangesAsync();
                targetEventId = babyEvent.BabyEvent.Id;
            });
            var output = await _entityAppService.GetStudyEvents(new GetStudyEventsInput()
            {
                BabyId = baby.Id,
                PlayerGuid = baby.Family.FatherId
            });
            output.StudyEmergencies.FirstOrDefault(s => s.Event.Id == targetEventId).ShouldNotBeNull();
        }
        /// <summary>
        /// 应该获取学习突发事件通过事件触发
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Get_Emergencies_StudyEvent_By_Event_Trigger()
        {
            var babies = await GetAllBabies();
            var baby = babies.FirstOrDefault();
            var targetEventId = 4;

            var output = await _entityAppService.GetStudyEvents(new GetStudyEventsInput()
            {
                BabyId = baby.Id,
                PlayerGuid = baby.Family.FatherId
            });
            output.StudyEmergencies.Where(s => s.Event.Id == targetEventId).ShouldNotBeNull();
        }

        /// <summary>
        /// 应该获取学习突发事件通过属性触发
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Get_Emergencies_GroupEvent_By_Property_Trigger()
        {
            var babies = await GetAllBabies();
            var baby = babies.FirstOrDefault(s => s.GroupId == 2);
            var targetEventId = 31;
            await UsingDbContextAsync(async context =>
            {
                var familyItem = await context.Families.FindAsync(baby.FamilyId);
                var babyEvent = await context.EventGroupBabyEvents.Include(s => s.BabyEvent).Where(s => s.BabyEvent.Type == IncidentType.Growup && s.GroupId == baby.GroupId && s.BabyEvent.ConditionType == ConditionType.Property && s.BabyEvent.BabyProperty == BabyProperty.Happiness).FirstOrDefaultAsync();
                familyItem.Happiness = Convert.ToInt32(babyEvent.BabyEvent.MaxValue) + 1;
                context.Entry(familyItem).State = EntityState.Modified;
                await context.SaveChangesAsync();
                targetEventId = babyEvent.BabyEvent.Id;
            });
            var output = await _entityAppService.GetGrowUpEvents(new GetGrowUpEventsInput()
            {
                BabyId = baby.Id,
                PlayerGuid = baby.Family.FatherId
            });
            output.GrowUpEmergencies.FirstOrDefault(s => s.Event.Id == targetEventId).ShouldNotBeNull();
        }
        /// <summary>
        /// 应该获取学习突发事件通过事件触发
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Get_Emergencies_GroupEvent_By_Event_Trigger()
        {
            var babies = await GetAllBabies();
            var baby = babies.FirstOrDefault();
            var targetEventId = 33;

            var output = await _entityAppService.GetGrowUpEvents(new GetGrowUpEventsInput()
            {
                BabyId = baby.Id,
                PlayerGuid = baby.Family.FatherId
            });
            output.GrowUpEmergencies.Where(s => s.Event.Id == targetEventId).ShouldNotBeNull();
        }

        /// <summary>
        /// 成长记录不应该为空
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BabyGrowUpRecord_ShouldNotBe_Null()
        {
            var babies = await GetAllBabies();
            var baby = babies.LastOrDefault();
            await _memoryCache.SetStringAsync("playerLastLoginTime" + baby.Family.FatherId.ToString(), DateTime.Now.AddMinutes(-3).ToString());

            await UsingDbContextAsync(async context =>
            {
                await context.BabyGrowUpRecords.AddAsync(new BabyGrowUpRecord()
                {
                    BabyId = baby.Id,
                    Imagine = 12,
                    Intelligence = 5
                });
            });
            //fact
            var output = await _entityAppService.BabyGrowUpRecord(new BabyGrowUpRecordInput()
            {
                BabyId = baby.Id,
                PlayerGuid = baby.Family.FatherId
            });
            //assert
            output.ShouldNotBeNull();
        }
        /// <summary>
        /// 根据宝宝是第几胎确定消耗的金币系数
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BirthOrder_Not_First_Ensume_Coin_ShouldBe_Double()
        {
            var babies = await GetAllBabies();
            var baby = babies.FirstOrDefault(s => s.BirthOrder > 1);
            var output = await _entityAppService.GetStudyEvents(new GetStudyEventsInput
            {
                BabyId = baby.Id,
                PlayerGuid = baby.Family.FatherId
            });
            output.Setting.Coefficient.ShouldBeGreaterThan(1);
             UsingDbContext( context =>
            {
                output.StudyEvents.ForEach(async s => await s.Options.AsQueryable().ForEachAsync(async d =>
             {
                 var consume = await context.Rewards.FindAsync(d.Consume.Id);
                 d.Consume.CoinCount.ShouldBe(consume.CoinCount * baby.BirthOrder);
             }
            ));
            });
        }
    }
}
