using Abp;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MQKJ.BSMP.Tests.ChineseBaby
{
    /// <summary>
    /// 提交事件
    /// </summary>
    public class BabyEventAppService_EventHandle_ValidEventSubmit_Tests : BSMPTestBase
    {
        private readonly IBabyEventAppService _entityAppService;

        public BabyEventAppService_EventHandle_ValidEventSubmit_Tests()
        {
            _entityAppService = Resolve<IBabyEventAppService>();
        }
        /// <summary>
        /// 判断是否已提交或处于CD
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ValidEventSubmit_EventHasBeenSubmited_Should_NotAllowed_Submit()
        {
            //获取请求参数
            var input = await GetHandleEventInput();
            //准备
            await UsingDbContextAsync(async context =>
            {
                var baby = await context.Babies.FirstOrDefaultAsync(s => s.Id == input.BabyId);
                var record = await context.BabyEventRecords.FirstOrDefaultAsync(s => s.BabyId == input.BabyId && s.EventId == input.EventId);
                var currentTime = DateTime.UtcNow.AddSeconds(int.MaxValue);
                var currentTimeStamp = new DateTimeOffset(currentTime);
                if (record == null)
                {
                    record = new BabyEventRecord()
                    {
                        EndTimeStamp = currentTimeStamp.ToUnixTimeSeconds(),
                        EventId = input.EventId,
                        PlayerId = input.PlayerGuid,
                        FamilyId = input.FamilyId,
                        OptionId = input.OptionId,
                        State = EventRecordState.Handling,
                        BabyId = input.BabyId,
                        GroupId = Convert.ToInt32(baby.GroupId)
                    };
                    context.BabyEventRecords.Add(record);
                    context.Entry(record).State = EntityState.Added;
                    await context.SaveChangesAsync();
                }
            });
            //fact
            await _entityAppService.ValidEventSubmit(input).ShouldThrowAsync<AbpException>("事件已提交，处于CD期");
        }
        /// <summary>
        /// 判断金钱是否够消耗用
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ValidEventSubmit_EventConsumeMoneyNotEnough_Should_NotAllowed_Submit()
        {
            //获取请求参数
            var input = await GetHandleEventInput();
            //准备
            await UsingDbContextAsync(async context =>
            {
                var record = await context.BabyEventRecords.FirstOrDefaultAsync(s => s.BabyId == input.BabyId && s.EventId == input.EventId);
                var babyEvent = await context.BabyEvents.Include(s => s.Options).ThenInclude(s => s.Consume).FirstOrDefaultAsync(s => s.Id == input.EventId);
                var consume = babyEvent.Options.FirstOrDefault(s => s.Id == input.OptionId)?.Consume;
                consume.CoinCount = int.MaxValue;
                context.Entry(consume).State = EntityState.Modified;
                await context.SaveChangesAsync();
            });
            //fact
            await _entityAppService.ValidEventSubmit(input).ShouldThrowAsync<AbpException>("宝宝金钱不够消耗");
        }

        /// <summary>
        /// 判断精力是否够消耗用
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ValidEventSubmit_EventConsumeEnergyNotEnough_Should_NotAllowed_Submit()
        {
            //获取请求参数
            var input = await GetHandleEventInput();
            //准备
            await UsingDbContextAsync(async context =>
            {
                var record = await context.BabyEventRecords.FirstOrDefaultAsync(s => s.BabyId == input.BabyId && s.EventId == input.EventId);
                var babyEvent = await context.BabyEvents.Include(s => s.Consume).FirstOrDefaultAsync(s => s.Id == input.EventId);
                babyEvent.Consume.CoinCount = 0;
                babyEvent.Consume.Energy = int.MaxValue;
                context.Entry(babyEvent.Consume).State = EntityState.Modified;
                await context.SaveChangesAsync();
            });
            //fact
            await _entityAppService.ValidEventSubmit(input).ShouldThrowAsync<AbpException>("宝宝精力不够消耗");
        }
        /// <summary>
        /// 判断是否超过最大学习次数
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ValidEventSubmit_EventBeMaxAllowedStudyCount_Should_NotAllowed_Submit()
        {
            //获取请求参数
            var input = await GetHandleEventInput();
            //准备
            await UsingDbContextAsync(async context =>
            {
                var baby = await context.Babies.Include(s => s.Family).FirstOrDefaultAsync(s => s.Id == input.BabyId);
                var babyEvent = await context.BabyEvents.Include(s => s.Consume).FirstOrDefaultAsync(s => s.Type == IncidentType.Study && s.Consume.CoinCount < baby.Family.Deposit && s.Consume.Energy < baby.Energy);
                input.EventId = babyEvent.Id;
                var currentTime = DateTime.UtcNow.AddSeconds(int.MaxValue);
                var currentTimeStamp = new DateTimeOffset(currentTime);
                var record = new BabyEventRecord()
                {
                    EndTimeStamp = currentTimeStamp.ToUnixTimeSeconds(),
                    EventId = input.EventId,
                    PlayerId = input.PlayerGuid,
                    FamilyId = input.FamilyId,
                    OptionId = input.OptionId,
                    State = EventRecordState.Handling,
                    BabyId = input.BabyId
                };
                context.BabyEventRecords.Add(record);
                context.Entry(record).State = EntityState.Added;
                await context.SaveChangesAsync();
            });
            //fact
            await _entityAppService.ValidEventSubmit(input).ShouldThrowAsync<AbpException>("超过最大可学习次数");
        }
        /// <summary>
        /// 判断当事件已完成（非学习型事件）时，不可重复提交
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ValidEventSubmit_NotStudyEvent_Should_Only_Allowed_Submit_OneTime()
        {
            //获取请求参数
            var input = await GetHandleEventInput();
            //准备
            await UsingDbContextAsync(async context =>
            {
                var baby = await context.Babies.Include(s => s.Family).FirstOrDefaultAsync(s => s.Id == input.BabyId);
                var babyEvent = await context.BabyEvents.Include(s => s.Consume).FirstOrDefaultAsync(s => s.Type == IncidentType.Growup && s.Consume.CoinCount < baby.Family.Deposit && s.Consume.Energy < baby.Energy);
                input.EventId = babyEvent.Id;
                var currentTime = DateTime.UtcNow.AddSeconds(int.MaxValue);
                var currentTimeStamp = new DateTimeOffset(currentTime);
                var record = new BabyEventRecord()
                {
                    EndTimeStamp = currentTimeStamp.ToUnixTimeSeconds(),
                    EventId = input.EventId,
                    PlayerId = input.PlayerGuid,
                    FamilyId = input.FamilyId,
                    OptionId = input.OptionId,
                    State = EventRecordState.Handling,
                    BabyId = input.BabyId
                };
                context.BabyEventRecords.Add(record);
                context.Entry(record).State = EntityState.Added;
                await context.SaveChangesAsync();
            });
            //fact
            await _entityAppService.ValidEventSubmit(input).ShouldThrowAsync<AbpException>("事件已完成");

        }
        /// <summary>
        /// 判断是否存在阻断事件
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ValidEventSubmit_ExsitBlockingEvent_Should_NotAllowed_Submit()
        {
            //获取请求参数
            var input = await GetHandleEventInput(3);
            //准备
            await UsingDbContextAsync(async context =>
            {
                var baby = await context.Babies.FindAsync(input.BabyId);
                var currentTime = DateTime.UtcNow.AddSeconds(int.MaxValue);
                var currentTimeStamp = new DateTimeOffset(currentTime);
                //2组
                var babyBlockEvent = await context.EventGroupBabyEvents.Include(s => s.BabyEvent).FirstOrDefaultAsync(s => s.GroupId == baby.GroupId && s.BabyEvent.Type == IncidentType.Block);
                //- 判断阻断事件是否已处理（获取未结束的阻断事件）
                var blockEventRecordCount = await context.BabyEventRecords.CountAsync(s => s.BabyId == input.BabyId && s.EventId == babyBlockEvent.EventId && s.GroupId == baby.GroupId && s.EndTimeStamp > currentTimeStamp.ToUnixTimeSeconds());
                //增加阻断性事件的记录
                if (blockEventRecordCount == 0)
                {
                    int blockEventId = babyBlockEvent.EventId;
                    var blockEventRecord = new BabyEventRecord()
                    {
                        EndTimeStamp = currentTimeStamp.ToUnixTimeSeconds(),
                        EventId = blockEventId,
                        PlayerId = input.PlayerGuid,
                        FamilyId = input.FamilyId,
                        OptionId = input.OptionId,
                        State = EventRecordState.Handling,
                        BabyId = input.BabyId,
                        GroupId = Convert.ToInt32(baby.GroupId)
                    };
                    //把事件的消耗给设置为最低，避免出错
                    var babyEvent = await context.BabyEvents.Include(s => s.Consume).FirstOrDefaultAsync(s => s.Id == input.EventId);
                    babyEvent.Consume.CoinCount = 0;
                    babyEvent.Consume.Energy = 0;
                    context.Entry(babyEvent.Consume).State = EntityState.Modified;
                    context.BabyEventRecords.Add(blockEventRecord);
                    context.Entry(blockEventRecord).State = EntityState.Added;
                    await context.SaveChangesAsync();
                }
            });
            //fact
            await _entityAppService.ValidEventSubmit(input).ShouldThrowAsync<AbpException>("存在阻断性事件");
        }
        /// <summary>
        /// 判断是否事件过期
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ValidEventSubmit_EventHasExpired_Should_NotAllowed_Submit()
        {
            //获取请求参数
            var input = await GetHandleEventInput();
            //准备
            await UsingDbContextAsync(async context =>
            {
                //获取特殊事件
                var specialEvent = await context.BabyEvents.Include(s => s.Options).FirstOrDefaultAsync(s => s.Type == IncidentType.Special && s.ExpirationGroupId != null);
                input.EventId = specialEvent.Id;
                input.OptionId = Convert.ToInt32(specialEvent.Options.FirstOrDefault()?.Id);
                var babyEvent = await context.BabyEvents.FirstOrDefaultAsync(s => s.Id == input.EventId);
                var baby = await context.Babies.FindAsync(input.BabyId);
                baby.Energy = int.MaxValue;
                baby.GroupId = babyEvent.ExpirationGroupId + 1;
                context.Entry(baby).State = EntityState.Modified;
                await context.SaveChangesAsync();
            });
            //fact
            await _entityAppService.ValidEventSubmit(input).ShouldThrowAsync<AbpException>("事件已过期");
        }

        /// <summary>
        /// 获取默认处理事件参数
        /// </summary>
        /// <returns></returns>
        private async Task<HandleEventInput> GetHandleEventInput(int babyId = 1)
        {
            var handleEventInput = new HandleEventInput();
            await UsingDbContextAsync(async context =>
            {
                var baby = await context.Babies.Include(s => s.Family).FirstOrDefaultAsync(s => s.Id == babyId);
                var events = context.BabyEvents.ToList();
                var babyEvent = await context.BabyEvents.Include(s => s.Options).Include(s => s.BabyEventRecords).FirstOrDefaultAsync(s => s.Id == 1);
                handleEventInput = new HandleEventInput()
                {
                    BabyId = baby.Id,
                    EventId = 1,
                    FamilyId = baby.FamilyId,
                    OptionId = Convert.ToInt32(babyEvent.Options.FirstOrDefault()?.Id),
                    PlayerGuid = baby.Family.FatherId,
                    TheOtherGuid = baby.Family.MotherId
                };
            });
            return handleEventInput;
        }
    }
}
