using Abp;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MQKJ.BSMP.Tests.ChineseBaby
{
    /// <summary>
    /// 提交事件
    /// </summary>
    public class BabyEventAppService_EventHandle_Tests : BSMPTestBase
    {
        private readonly IBabyEventAppService _entityAppService;

        public BabyEventAppService_EventHandle_Tests()
        {
            _entityAppService = Resolve<IBabyEventAppService>();
        }
        /// <summary>
        /// 模板
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ValidEventSubmit_EventHasBeenSubmited_Should_NotAllowed_Submit()
        {
            //准备数据
            var babies = await GetAllBabies();

            var baby = babies.FirstOrDefault();
            //
            //fact
            //baby

        }
        /// <summary>
        /// 继续成长
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BabyEvent_Should_GoTo_Next_EventGroup()
        {
            //准备数据
            var babies = await GetAllBabies();

            var baby = babies.FirstOrDefault();
            //
            //fact
            var output = await _entityAppService.BabyGoOnGrowUp(new BabyGoOnGrowUpInput()
            {
                BabyId = baby.Id,
                PlayerGuid = baby.Family.FatherId
            });
            output.StroyEnding.ShouldBeNull();
        }
        /// <summary>
        /// 继续成长
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BabyGoOnGrowUp_ShouldThrow_AbpException()
        {
            //准备数据
            var babies = await GetAllBabies();

            var baby = babies.FirstOrDefault();
            await UsingDbContextAsync(async context =>
            {
                baby.State = BabyState.Adult;
                context.Entry(baby).State = EntityState.Modified;
                await context.SaveChangesAsync();
            });
            //fact
            await _entityAppService.BabyGoOnGrowUp(new BabyGoOnGrowUpInput()
            {
                BabyId = baby.Id,
                PlayerGuid = baby.Family.FatherId
            }).ShouldThrowAsync<AbpException>("宝宝已经成人");
            //assert
        }
        /// <summary>
        /// 继续成长
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BabyGoOnGrowUp_StoryEnding_ShouldNotBe_Null()
        {
            //准备数据
            var babies = await GetAllBabies();

            var baby = babies.FirstOrDefault();
            await UsingDbContextAsync(async context =>
            {
                var lastGroup = context.EventGroups.FirstOrDefaultAsync(s => s.PrevGroupId == null);
                //清除所有事件
                await context.BabyEventRecords.Where(s => s.BabyId == baby.Id && s.GroupId == baby.GroupId).ForEachAsync(async s =>
                      {
                          s.State = EventRecordState.Handled;
                          context.BabyEventRecords.UpdateRange(s);
                          await context.SaveChangesAsync();
                      });
                baby.GroupId = lastGroup?.Id;
                context.Entry(baby).State = EntityState.Modified;
                await context.SaveChangesAsync();
            });
            //fact
            var output = await _entityAppService.BabyGoOnGrowUp(new BabyGoOnGrowUpInput()
            {
                BabyId = baby.Id,
                PlayerGuid = baby.Family.FatherId
            });
            //assert
            output.StroyEnding.ShouldNotBeNull();
        }

    }
}
