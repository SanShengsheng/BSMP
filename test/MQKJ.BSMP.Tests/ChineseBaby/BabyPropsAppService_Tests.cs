using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Backpack;
using Should;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MQKJ.BSMP.Tests.ChineseBaby
{
    public class BabyPropsAppService_Tests : BSMPTestBase
    {
        private readonly IBabyPropAppService _entityAppService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<BabyProp> _babyPropsRepository;
        private readonly IRepository<ChineseBabies.Baby> _babyRepository;
        private readonly IRepository<BabyFamilyAsset, Guid> _babyFamilyAssetRepository;
        private readonly IRepository<Family> _familyRepository;
        private readonly ITestOutputHelper _testOutputHelper;


        public BabyPropsAppService_Tests(ITestOutputHelper testOutputHelper)
        {
            _unitOfWorkManager = Resolve<IUnitOfWorkManager>();
            _entityAppService = Resolve<IBabyPropAppService>();
            _testOutputHelper = testOutputHelper;

            _babyPropsRepository = Resolve<IRepository<BabyProp>>();
            _babyRepository = Resolve<IRepository<ChineseBabies.Baby>>();
            _babyFamilyAssetRepository = Resolve<IRepository<BabyFamilyAsset, Guid>>();
            _familyRepository = Resolve<IRepository<Family>>();
        }

        /// <summary>
        /// 第x(x>1)胎宝宝购买道具
        /// 测试点：二胎再购买一胎买过的道具
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Buy_Prop_When_Baby_NotIs_FirstBaby()
        {
            //var babies = await GetAllBabies();
            var families = await GetAllFamilies();
            var family = families.FirstOrDefault(s => s.Babies.Count > 1);
            //  准备环境
            // 成年的宝宝
            var babyAdult = family.Babies.FirstOrDefault(s => s.State == BabyState.Adult);
            babyAdult.ShouldNotBeNull();
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {
                // 道具
                var skinProp = await _babyPropsRepository.GetAllIncluding(s => s.BabyPropType).Include(s => s.Prices).AsNoTracking().FirstOrDefaultAsync(s => s.BabyPropType.Name == "Skin");
                var houseProp = await _babyPropsRepository.GetAllIncluding(s => s.BabyPropType).Include(s => s.Prices).AsNoTracking().FirstOrDefaultAsync(s => s.BabyPropType.Name == "House");

                family.Deposit = family.Deposit + 1e10;
                _familyRepository.Update(family);
                skinProp.ShouldNotBeNull();
                houseProp.ShouldNotBeNull();
                // 成年的宝宝买道具 
                await _entityAppService.PostBuyPropAsync(new PostBuyPropInput()
                {
                    BabyId = babyAdult.Id,
                    FamilyId = family.Id,
                    PlayerGuid = family.FatherId,
                    PriceId = skinProp.Prices.LastOrDefault(s => s.Validity == -1).Id,
                    PropId = skinProp.Id
                });
                await _entityAppService.PostBuyPropAsync(new PostBuyPropInput()
                {
                    BabyId = babyAdult.Id,
                    FamilyId = family.Id,
                    PlayerGuid = family.FatherId,
                    PriceId = houseProp.Prices.LastOrDefault(s => s.Validity == -1).Id,
                    PropId = houseProp.Id
                });

                // fact
                var babyUnderage = family.Babies.FirstOrDefault(s => s.State == BabyState.UnderAge);
                babyUnderage.ShouldNotBeNull();
                var buySkinPropResponse = await _entityAppService.PostBuyPropAsync(new PostBuyPropInput()
                {
                    BabyId = babyUnderage.Id,
                    FamilyId = family.Id,
                    PlayerGuid = family.FatherId,
                    PriceId = skinProp.Prices.LastOrDefault(s => s.Validity == -1).Id,
                    PropId = skinProp.Id
                });
                var buyHousePropResponse = await _entityAppService.PostBuyPropAsync(new PostBuyPropInput()
                {
                    BabyId = babyUnderage.Id,
                    FamilyId = family.Id,
                    PlayerGuid = family.FatherId,
                    PriceId = houseProp.Prices.LastOrDefault(s => s.Validity == -1).Id,
                    PropId = houseProp.Id
                });
                buySkinPropResponse.ShouldNotBeNull();
                buyHousePropResponse.ShouldNotBeNull();
            };

        }

        /// <summary>
        /// 道具购买后应该触发下一个道具显示，eg：以皮肤为例
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ShouldBe_TriggerNextPropShown_By_LastPropBuy()
        {
            var families = await GetAllFamilies();
            var family = families.FirstOrDefault();
            //  准备环境
            // 成年的宝宝
            var babyAdult = family.Babies.FirstOrDefault();
            babyAdult.ShouldNotBeNull();
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {
                var propssss = _babyPropsRepository.GetAllIncluding(s => s.BabyPropType).Where(s => s.BabyPropType.Name == "Skin" );
                // 道具
                var skinProp = await _babyPropsRepository.GetAllIncluding(s => s.BabyPropType).Include(s => s.Prices).AsNoTracking().OrderBy(s=>s.Code).FirstOrDefaultAsync(s => s.BabyPropType.Name == "Skin"&&s.TriggerShowPropCode!=null);
                var nextSkinProp = await _babyPropsRepository.GetAll().FirstOrDefaultAsync(s => s.Code == skinProp.TriggerShowPropCode);
                family.Deposit = family.Deposit + 1e10;
                _familyRepository.Update(family);
                skinProp.ShouldNotBeNull();
                nextSkinProp.ShouldNotBeNull();
                // 成年的宝宝买道具 
                await _entityAppService.PostBuyPropAsync(new PostBuyPropInput()
                {
                    BabyId = babyAdult.Id,
                    FamilyId = family.Id,
                    PlayerGuid = family.FatherId,
                    PriceId = skinProp.Prices.LastOrDefault(s => s.Validity == -1).Id,
                    PropId = skinProp.Id
                });
                unitOfWork.Complete();
                //Fact 
                var props = await _entityAppService.GetPage(new ChineseBabies.Dtos.GetBabyPropsInput()
                {
                    BabyId = babyAdult.Id,
                    FamilyId = family.Id,
                    Gender = babyAdult.Gender,
                    PropTypeId = (int)skinProp.BabyPropTypeId,
                });
                props.Items.Count.ShouldEqual(2);
                props.Items.Any(s => s.BasicInfo.Id == nextSkinProp.Id).ShouldBeTrue();
            }
         
        }
    }
}
