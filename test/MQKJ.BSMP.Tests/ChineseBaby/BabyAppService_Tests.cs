using Abp;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MQKJ.BSMP.Tests.Baby
{
    public class BabyAppService_Tests : BSMPTestBase
    {
        private readonly IBabyAppService _entityAppService;

        public BabyAppService_Tests()
        {
            _entityAppService = Resolve<IBabyAppService>();

        }
        /// <summary>
        ///  获取宝宝信息
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Get_GetBabyBasicInfo()
        {
            // Act
            var babies = await GetAllBabies();

            var output = await _entityAppService.GetBabyBasicInfo(
                new GetBabyBasicInfoInput()
                {
                    BabyId = babies.FirstOrDefault().Id
                }
                );

            //// Assert
            output.ShouldNotBeNull();
        }
        /// <summary>
        /// 获取宝宝出生记录
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Get_BabyBirthRecord()
        {
            // Act
            var babies = await GetAllBabies();

            var output = await _entityAppService.BabyBirthRecord(
                new BabyBirthRecordInput()
                {
                    BabyId = babies.FirstOrDefault().Id
                }
                );

            //// Assert
            output.ShouldNotBeNull();
        }
        /// <summary>
        /// 获取玩家所有宝宝
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Get_Babies()
        {
            // Act
            var players = await GetAllPlayers();

            var output = await _entityAppService.GetBabies(
                new GetBabiesInput()
                {
                    PlayerGuid = players.FirstOrDefault().Id,
                });

            //// Assert
            output.ShouldNotBeNull();
        }
        /// <summary>
        /// 宝宝第一次取名（父亲）
        /// 妈妈修改名字 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Get_Baby_First_New_Name()
        {
            //act 
            var babies = await GetAllBabies();
            var baby = babies.FirstOrDefault();
            //assert
            //爸爸第一次取名
            await _entityAppService.GiveBabyName(new GiveBabyNameInput
            {
                PlayerGuid = baby.Family.FatherId,
                BabyId = baby.Id,
                NewName = "Lebron",
            });
            await UsingDbContextAsync(context =>
            {
                var babyNewName = context.Babies.Find(baby.Id).Name;
                babyNewName.ShouldBe("Lebron");
                return Task.CompletedTask;
            });
            //妈妈修改
            await _entityAppService.GiveBabyName(new GiveBabyNameInput
            {
                PlayerGuid = baby.Family.FatherId,
                BabyId = baby.Id,
                NewName = "John",
            });
            await UsingDbContextAsync(context =>
            {
                var babyNewName = context.Babies.Find(baby.Id).Name;
                babyNewName.ShouldBe("John");
                context.Informations.Where(s => s.Type == InformationType.Barrage && s.Content.Contains("出生了")).ShouldNotBeEmpty();
                return Task.CompletedTask;
            });
        }
        /// <summary>
        ///  母亲修改宝宝名字（孩子还没有取名）
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Not_Get_Baby_First_New_Name_By_Mother()
        {
            //act 
            var babies = await GetAllBabies();
            var baby = babies.LastOrDefault();//为了避免别的测试影响，使用第二个宝宝

            //assert
            await _entityAppService.GiveBabyName(new GiveBabyNameInput
            {
                PlayerGuid = baby.Family.MotherId,
                BabyId = baby.Id,
                NewName = "Kevin",
            }).ShouldThrowAsync<UserFriendlyException>(BSMPErrorCodes.FirstGiveNameOnlyAllowByFather.ToString());

        }
        /// <summary>
        ///  母亲修改宝宝名字（孩子已取名取名），金币不够
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_New_Name_By_Mother_Coin_Not_Enough()
        {
            //act 
            var babies = await GetAllBabies();
            var baby = babies.FirstOrDefault();
                                              //准备数据
            await UsingDbContextAsync(async context =>
            {
                baby.Family.Deposit = 99;
                context.Entry(baby.Family).State = EntityState.Modified;
                await context.SaveChangesAsync();
            });
            //assert
            await _entityAppService.GiveBabyName(new GiveBabyNameInput
            {
                PlayerGuid = baby.Family.MotherId,
                BabyId = baby.Id,
                NewName = "Kevin",
            }).ShouldThrowAsync<AbpException>("金币不够");

        }
        /// <summary>
        ///  母亲修改宝宝名字（孩子已取名取名），扣掉金币
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Rename_By_Father()
        {
            //act 
            var babies = await GetAllBabies();
            var baby = babies.FirstOrDefault();//为了避免别的测试影响，使用第二个宝宝
                                              //准备数据
            await UsingDbContextAsync(async context =>
            {
                baby.Family.Deposit = 5000;
                baby.Name = "阿丑";
                context.Entry(baby.Family).State = EntityState.Modified;
                context.Entry(baby).State = EntityState.Modified;
                await context.SaveChangesAsync();
            });
            //assert
            var output = await _entityAppService.GiveBabyName(new GiveBabyNameInput
            {
                PlayerGuid = baby.Family.FatherId,
                BabyId = baby.Id,
                NewName = "Kevin",
            });
            output.ShouldNotBeNull();
            await UsingDbContextAsync(async context =>
            {
               (await context.Families.FirstOrDefaultAsync(s=>s.Id==baby.FamilyId)).Deposit.ShouldBeLessThanOrEqualTo(4900);
            });
        }
        /// <summary>
        /// 敏感词
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Not_Get_Baby_New_SensitiveWord_Name()
        {
            //act 
            var babies = await GetAllBabies();
            var baby = babies.LastOrDefault();//为了避免别的测试影响，使用第二个宝宝

            //assert
            await _entityAppService.GiveBabyName(new GiveBabyNameInput
            {
                PlayerGuid = baby.Family.FatherId,
                BabyId = baby.Id,
                NewName = "测试专用",
            }).ShouldThrowAsync<UserFriendlyException>(BSMPErrorCodes.FirstGiveNameOnlyAllowByFather.ToString());

        }
       
        /// <summary>
        /// 查看宝宝出生动画
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Update_Baby_LookBirthMovie()
        {
            var baby = (await GetAllBabies()).FirstOrDefault();
            var output = _entityAppService.LookBirthMovie(
          new LookBirthMovieInput
          {
              BabyId = baby.Id,
              PlayerGuid = baby.Family.FatherId
          });
            await UsingDbContextAsync(context =>
            {
                context.Babies.Find(baby.Id).IsWatchBirthMovieFather.ShouldBeTrue();
                return Task.CompletedTask;
            });
        }
    }
}
