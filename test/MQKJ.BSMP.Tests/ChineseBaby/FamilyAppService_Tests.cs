using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Backpack;
using MQKJ.BSMP.ChineseBabies.Families.Dtos;
using MQKJ.BSMP.ChineseBabies.HostDtos.FamilyDto;
using MQKJ.BSMP.QCloud;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MQKJ.BSMP.Tests.Families
{
    public class FamilyAppService_Tests : BSMPTestBase
    {
        private readonly IFamilyAppService _entityAppService;
        private readonly IQCloudApiClient _qcouldApiClient;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<BabyProp> _babyPropsRepository;
        private readonly IRepository<BabyFamilyAsset, Guid> _babyFamilyAssetRepository;
        private readonly IRepository<ChineseBabies.Baby> _babyRepository;

        public FamilyAppService_Tests()
        {
            _unitOfWorkManager = Resolve<IUnitOfWorkManager>();
            _qcouldApiClient = Resolve<IQCloudApiClient>();

            _entityAppService = Resolve<IFamilyAppService>();
            _babyPropsRepository = Resolve<IRepository<BabyProp>>();
            _babyFamilyAssetRepository = Resolve<IRepository<BabyFamilyAsset, Guid>>();
            _babyRepository = Resolve<IRepository<ChineseBabies.Baby>>();
        }
        /// <summary>
        /// 应该获取新创建的家庭信息
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Get_Family()
        {
            // Act

            var families = await GetAllFamilies();

            var output = await _entityAppService.GetFamily(new GetFamilyInput { FamilyId = families.FirstOrDefault().Id });

            //// Assert
            output.ShouldNotBeNull();
        }
        /// <summary>
        /// 测试正常情况，邀请人的身份是父亲
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Create_Family()
        {
            // Act
            var _playerGuid = Guid.NewGuid();
            var _inviterPlayerGuid = Guid.NewGuid();
            var result = await _entityAppService.CreateFamily(
                     new CreateFamilyInput
                     {
                         PlayerGuid = _playerGuid,
                         InviterPlayerGuid = _inviterPlayerGuid,
                         InviterFamilyIdentity = FamilyIdentity.Dad
                     });
            result.BabyId.ShouldBeGreaterThan(0);
            result.FamilyId.ShouldBeGreaterThan(0);
            await UsingDbContextAsync(async context =>
            {
                //家庭
                var testFamily = await context.Families.FirstOrDefaultAsync(u => u.MotherId == _playerGuid && u.FatherId == _inviterPlayerGuid);
                //职业
                //爸爸职业
                var testFatherProfession = await context.PlayerProfessions.FirstOrDefaultAsync(s => s.FamilyId == testFamily.Id && s.PlayerId == testFamily.FatherId);
                //妈妈职业
                var testMotherProfession = await context.PlayerProfessions.FirstOrDefaultAsync(s => s.FamilyId == testFamily.Id && s.PlayerId == testFamily.MotherId);
                //宝宝
                var testBaby = await context.Babies.FirstOrDefaultAsync(s => s.FamilyId == testFamily.Id && s.BirthOrder == 1);
                testFamily.ShouldNotBeNull();
                testFatherProfession.ShouldNotBeNull();
                testMotherProfession.ShouldNotBeNull();
                testBaby.ShouldNotBeNull();
                testBaby.Healthy.ShouldBe(100);
                testBaby.Energy.ShouldBe(100);
            });
        }
        /// <summary>
        /// 生宝宝  最后的宝宝未成年（二胎）
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Create_New_Baby_When_Baby_UnderAge()
        {
            // Act
            var babies = await GetAllBabies();
            var baby = babies.FirstOrDefault(s => s.State == BabyState.UnderAge);
            //宝宝尚未成年的情况
            await _entityAppService.BirthNewBaby(
                new BirthNewBabyInput
                {
                    FamilyId = baby.Family.Id,
                    PlayerGuid = baby.Family.FatherId,
                }).ShouldThrowAsync<UserFriendlyException>("存在尚未成年的宝宝！");
        }
        /// <summary>
        /// 生宝宝  最后的宝宝【已】成年（二胎）
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Create_New_Baby_When_Baby_Adult()
        {
            // Act
            var babies = await GetAllBabies();
            var baby = babies.GroupBy(s => s.FamilyId).Select(s => s.LastOrDefault(d => d.State == BabyState.Adult)).FirstOrDefault();
            var paras = new BirthNewBabyInput();
            if (baby == null)
            {
                // 不存在最后一胎宝宝为成年宝宝的家庭！
                // 创建家庭，将家庭当前宝宝置为已成年
                var _playerGuid = Guid.NewGuid();
                var _inviterPlayerGuid = Guid.NewGuid();
                var family = await _entityAppService.CreateFamily(
                         new CreateFamilyInput
                         {
                             PlayerGuid = _playerGuid,
                             InviterPlayerGuid = _inviterPlayerGuid,
                             InviterFamilyIdentity = FamilyIdentity.Dad
                         });
                await UsingDbContextAsync(async context =>
                {
                    var newbornBaby = await context.Babies.FindAsync(family.BabyId);
                    newbornBaby.State = BabyState.Adult;
                    context.SaveChanges();
                });
                paras.FamilyId = family.FamilyId;
                paras.PlayerGuid = _playerGuid;
            }
            else
            {
                paras = new BirthNewBabyInput
                {
                    FamilyId = baby.Family.Id,
                    PlayerGuid = baby.Family.FatherId,
                };
            }

            var result = await _entityAppService.BirthNewBaby(
                   new BirthNewBabyInput
                   {
                       FamilyId = paras.FamilyId,
                       PlayerGuid = paras.PlayerGuid,
                   });
            await UsingDbContextAsync(context =>
            {
                var output = context.Babies.Find(result.BabyId);
                output.ShouldNotBeNull();
                return Task.CompletedTask;
            });
        }

        /// <summary>
        /// 获取家庭信息
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Get_FamilyState()
        {
            var babies = await GetAllBabies();
            var baby = babies.FirstOrDefault();
            var output = await _entityAppService.GetFamilyState(new GetFamilyStateInput()
            {
                FatherId = baby.Family.FatherId,
                MotherId = baby.Family.MotherId
            });
            output.isCreatedFamily.ShouldBeTrue();
            output.FamilyStateInfo.ShouldNotBeNull();
            output.FamilyStateInfo.LastBaby.ShouldNotBeNull();
        }
        /// <summary>
        /// 测试家庭信息接口，档次
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Get_FamilyInfo_Level()
        {
            var babies = await GetAllBabies();
            var baby = babies.FirstOrDefault();
            //低档
            await UsingDbContextAsync(context =>
            {
                var family = context.Families.Find(baby.FamilyId);
                family.Deposit = 600000;
                context.Entry(family).State = EntityState.Modified;
                context.SaveChanges();
                return Task.CompletedTask;
            });
            var output = await _entityAppService.GetFamily(new GetFamilyInput()
            {
                FamilyId = baby.FamilyId,
            });
            output.Level.ShouldBe(FamilyLevel.Poor);
            //中档
            await UsingDbContextAsync(async context =>
            {
                var family = context.Families.Find(baby.FamilyId);
                family.Deposit = 3000000;
                context.Entry(family).State = EntityState.Modified;
                await context.SaveChangesAsync();
            });
            var output2 = await _entityAppService.GetFamily(new GetFamilyInput()
            {
                FamilyId = baby.FamilyId,
            });
            output2.Level.ShouldBe(FamilyLevel.WellOff);
            //高档
            await UsingDbContextAsync(async context =>
            {
                var family = context.Families.Find(baby.FamilyId);
                family.Deposit = 3000001;
                context.Entry(family).State = EntityState.Modified;
                await context.SaveChangesAsync();
            });
            var output3 = await _entityAppService.GetFamily(new GetFamilyInput()
            {
                FamilyId = baby.FamilyId,
            });
            output3.Level.ShouldBe(FamilyLevel.Rich);
        }

        /// <summary>
        /// 请求解散家庭
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RequestDismissFamily()
        {
            //测试 协议解散
            var family = (await GetAllFamilies()).FirstOrDefault();
            var result = await _entityAppService.RequestDismissFamily(new RequestDismissFamilyInput()
            {
                DismissType = DismissFamilyType.AgreementDismiss,
                FamilyId = family.Id,
                PlayerGuid = family.FatherId
            });

            result.ShouldNotBeNull();

            //测试 强制解散
            var result1 = await _entityAppService.RequestDismissFamily(new RequestDismissFamilyInput()
            {
                DismissType = DismissFamilyType.ForceDismiss,
                FamilyId = family.Id,
                PlayerGuid = family.FatherId
            });

            result1.PayOutput.PaySign.ShouldNotBeNull();
        }


        /// <summary>
        /// 取消或拒绝解散家庭
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CancelDismissFamily()
        {
            var family = (await GetAllFamilies()).FirstOrDefault();
            var result = await _entityAppService.CanceAndRefuselDismissFamily(new CancelDismissFamilyInput()
            {
                FamilyId = family.Id,
                PlayerGuid = family.FatherId
            });

            result.IsSuccess.ShouldBeTrue();
        }

        /// <summary>
        /// 同意解散家庭
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AgreeDismissFamily()
        {
            var family = (await GetAllFamilies()).FirstOrDefault();

            var result = await _entityAppService.ConfirmDismissFamily(new ConfirmDismissFamilyInput()
            {
                FamilyId = family.Id,
                PlayerGuid = family.FatherId
            });

            result.IsSuccess.ShouldBeTrue();
        }

        /// <summary>
        /// 刚出生的宝宝不应该有皮肤道具特性加成（因为当前皮肤是不可继承的）
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ShouldNotHave_SkinPropAddition_When_NewbornBaby()
        {
            var babies = await GetAllBabies();
            var baby = babies.GroupBy(s => s.FamilyId).Select(s => s.LastOrDefault(d => d.State != BabyState.Adult)).FirstOrDefault();
            //  准备环境
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {

                // 给一件皮肤道具
                var skinProp =await _babyPropsRepository.GetAllIncluding(s => s.BabyPropType).FirstOrDefaultAsync(s => s.BabyPropType.Name == "Skin");
                skinProp.ShouldNotBeNull();
                if (skinProp != null)
                {
                    _babyFamilyAssetRepository.Insert(new BabyFamilyAsset()
                    {
                        FamilyId = baby.FamilyId,
                        ExpiredDateTime = null,//永久
                        BabyPropId = skinProp.Id,
                        OwnId = baby.Id,
                        IsEquipmenting = true,
                    });
                }
                // 给一个房子
                var houseProp =await _babyPropsRepository.GetAllIncluding(s => s.BabyPropType).FirstOrDefaultAsync(s => s.BabyPropType.Name == "House");
                houseProp.ShouldNotBeNull();
                if (houseProp != null)
                {
                    _babyFamilyAssetRepository.Insert(new BabyFamilyAsset()
                    {
                        FamilyId = baby.FamilyId,
                        ExpiredDateTime = null,//永久
                        BabyPropId = houseProp.Id,
                        OwnId = baby.Id,
                        IsEquipmenting=true,
                    });
                }
                // 将宝宝置为成年
                baby.State = BabyState.Adult;
                _babyRepository.Update(baby);
                unitOfWork.Complete();
            };
            // 生二胎
            var result = await _entityAppService.BirthNewBaby(
                           new BirthNewBabyInput
                           {
                               FamilyId = baby.FamilyId,
                               PlayerGuid = baby.Family.FatherId
                           });
            // fact
            await UsingDbContextAsync(async context =>
            {
                context.BabyProps.ShouldNotBeNull();
                context.BabyFamilyAssets.ShouldNotBeNull();
                var assetFeature = await context.BabyAssetFeatures.FirstOrDefaultAsync(s => s.BabyId == result.BabyId);
                assetFeature.ShouldNotBeNull();// 最少有座房子
                assetFeature.AssetFeatureProperty.ShouldNotContain("IncreaseArenaFightWinProbability");
            });
        }
    }
}
