
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.DomainService;
using MQKJ.BSMP.ChineseBabies.PropMall;
using MQKJ.BSMP.Utils.Extensions;
using MQKJ.BSMP.ChineseBabies.Backpack;
using Abp;
using MQKJ.BSMP.ChineseBabies.Asset;
using Abp.Dependency;
using MQKJ.BSMP.ChineseBabies.PropMall.Props.Props;
using MQKJ.BSMP.ChineseBabies.PropMall.BabyPropTerms.Dtos;
using MQKJ.BSMP.ChineseBabies.PropMall.Props.Terms;
using Abp.Domain.Uow;
using MQKJ.BSMP.ChineseBabies.PropMall.BabyProps.Dtos.Custom;
using Abp.BackgroundJobs;
using Hangfire;
using MQKJ.BSMP.ChineseBabies.PropMall.BabyProps.Dtos;
using MQKJ.BSMP.ChineseBabies.PropMall.BabyPropPrices;
using Abp.Application.Services;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// BabyProp应用层服务的接口实现方法  
    ///</summary>
    public class BabyPropAppService : BsmpApplicationServiceBase<BabyProp, int, BabyPropEditDto, BabyPropEditDto, GetBabyPropsInput, BabyPropListDto>, IBabyPropAppService
    {
        private readonly IRepository<BabyProp, int> _entityRepository;
        private readonly IRepository<BabyPropTerm, int> _babyPropTermRepository;
        private readonly IRepository<BabyPropFeature, int> _babyPropFeatureRepository;
        private readonly IRepository<BabyFamilyAsset, Guid> _babyFamilyAssetRepository;
        private readonly IRepository<BabyPropBuyTermType, Guid> _babyPropBuyTermTypeRepository;
        private readonly IRepository<BabyPropRecord, Guid> _babyPropRecordRepository;
        private readonly IRepository<FamilyCoinDepositChangeRecord, Guid> _familyCoinDepositChangeRecordRepository;
        private readonly IRepository<Family, int> _familyRepository;
        private readonly IRepository<BabyAssetAward, Guid> _babyAssetAwardRepository;
        private readonly IIocResolver _iocResolver;
        private readonly IBabyPropTermApplicationService _babyPropBuyTermApplicationService;
        private readonly IBabyFamilyAssetAppService _babyFamilyAssetAppService;
        private readonly IRepository<Baby, int> _babyRepository;
        /// <summary>
        /// 构造函数 
        ///</summary>
        public BabyPropAppService(
        IRepository<BabyProp, int> entityRepository,
        IRepository<BabyPropTerm, int> babyPropTermRepository,
        IRepository<BabyPropFeature, int> babyPropFeatureRepository,
               IRepository<BabyFamilyAsset, Guid> babyFamilyAssetRepository,
        IRepository<BabyPropRecord, Guid> babyPropRecordRepository,
         IRepository<FamilyCoinDepositChangeRecord, Guid> familyCoinDepositChangeRecordRepository,
         IRepository<Family, int> familyRepository,
          IRepository<BabyAssetAward, Guid> babyAssetAwardRepository,
          IRepository<BabyPropBuyTermType, Guid> babyPropBuyTermTypeRepository
           , IIocResolver iocResolver
         , IRepository<Baby, int> babyRepository
         //, IRepository<PlayerProfession, int> playerProfessionRepository
         , IBabyPropTermApplicationService babyPropBuyTermApplicationService
            , IBabyFamilyAssetAppService babyFamilyAssetAppService
        ) : base(entityRepository)
        {
            _entityRepository = entityRepository;
            _babyPropTermRepository = babyPropTermRepository;
            _babyPropFeatureRepository = babyPropFeatureRepository;
            _babyFamilyAssetRepository = babyFamilyAssetRepository;
            _babyPropRecordRepository = babyPropRecordRepository;
            _familyCoinDepositChangeRecordRepository = familyCoinDepositChangeRecordRepository;
            _familyRepository = familyRepository;
            _babyAssetAwardRepository = babyAssetAwardRepository;
            _babyPropBuyTermTypeRepository = babyPropBuyTermTypeRepository;
            _iocResolver = iocResolver;
            _babyPropBuyTermApplicationService = babyPropBuyTermApplicationService;
            _babyRepository = babyRepository;
            _babyFamilyAssetAppService = babyFamilyAssetAppService;
        }

        public async Task<ICollection<GetFamilyPropBuyInfoOutput>> GetFamilyPropBuyInfo(GetFamilyPropBuyInfoInput input)
        {
            if (input.PropIds == null)
            {
                return new List<GetFamilyPropBuyInfoOutput>();
            }
            var response = new List<GetFamilyPropBuyInfoOutput>();
            var buyTermTypes = await _babyPropBuyTermTypeRepository.GetAllListAsync();

            // 获取家庭资产
            var familyAssetes = await _babyFamilyAssetRepository.GetAllIncluding()
                  .Where(s => input.PropIds.Contains(s.BabyPropId) && (s.ExpiredDateTime > DateTime.UtcNow || s.ExpiredDateTime == null) && s.FamilyId == input.FamilyId && s.OwnId == input.BabyId).ToListAsync();
            // 获取道具列表
            var props = await _entityRepository.GetAllIncluding(s => s.BabyPropType)
                .Include(s => s.BabyPropTerms)
                               .Where(s => input.PropIds.Contains(s.Id)).AsNoTracking().ToListAsync();
            // 获取所有条件类型
            props.ForEach(s =>
{
    var item = new GetFamilyPropBuyInfoOutput()
    {
        PropId = s.Id,
        IsHave = familyAssetes.Any(d => d.BabyPropId == s.Id),
        GetWay = s.GetWay,
        IsAllow = s.GetWay != GetWay.Arena,
        IsEquipmenting = familyAssetes.Any(d => d.BabyPropId == s.Id && d.FamilyId == input.FamilyId && d.IsEquipmenting && (d.ExpiredDateTime > DateTime.UtcNow || d.ExpiredDateTime == null))
    };
    var itemTerms = GetPropBuyTerms(new PropStaffBuyTermInput()
    {
        BabyId = input.BabyId,
        FamilyId = input.FamilyId,
        BabyProp = s,
        BabyPropBuyTermTypes = buyTermTypes,
    });
    item.IsUnlock = itemTerms == null ? false : !itemTerms.Any(d => d.IsSatisfy == false);
    item.Terms = itemTerms;
    response.Add(item);
});
            return response;

        }
        /// <summary>
        /// 获取道具（分页）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetBabyPropsOutput>> GetPage(GetBabyPropsInput input)
        {
            //已购买的
            var boughtPropTerms = _babyPropRecordRepository.GetAll()
                .Include(p => p.BabyProp)
                .Where(a => a.BabyPropBagId == null
                && (a.BabyId == input.BabyId)
                && a.BabyProp.BabyPropTypeId == input.PropTypeId
                 || (a.BabyProp.IsInheritAble && a.FamilyId == input.FamilyId))
                .WhereIf(input.Gender.HasValue, x => x.BabyProp.Gender == input.Gender || x.BabyProp.Gender == Gender.All)
                .Select(p => new { p.BabyProp.TriggerShowPropCode, p.BabyPropId, p.BabyProp.TriggerNextShowPropCode });
            var boughtPropIds = boughtPropTerms.Select(c => c.BabyPropId);
            var showPropCodes = boughtPropTerms.Select(c => c.TriggerShowPropCode);
            var showNextPropCodes = boughtPropTerms.Select(c => c.TriggerNextShowPropCode);
            var query = _entityRepository.GetAllIncluding(s => s.Prices)
                .Include(s => s.BabyPropTerms)
                .Include(s => s.BabyPropFeatures)
                .Include(s => s.BabyPropPropertyAward)
                .Include(s => s.BabyPropType)
                .Where(s => s.BabyPropTypeId == input.PropTypeId)
                .WhereIf(input.Gender.HasValue, x => x.Gender == input.Gender || x.Gender == Gender.All)
                .OrderBy(input.Sorting)
                .AsNoTracking();

            //var isSkin = query.Any(c => c.BabyPropType.Name == "Skin");
            //if (!isSkin)
            //{
            query = query
            .Where(
            s => (boughtPropIds.Contains(s.Id) || showPropCodes.Contains(s.Code) || showNextPropCodes.Contains(s.Code) || s.Level == PropLevel.First || s.Level == PropLevel.Second));
            //}
            //TODO:已经购买的排在队列最后
            //query.Where()

            var propCount = await query.Distinct().AsQueryable().CountAsync();
            if (input.PageSize > 0)
            {
                input.MaxResultCount = input.PageSize;
            }
            if (input.PageIndex > 0)
            {
                input.SkipCount = (input.PageIndex - 1) * input.PageSize;
            }
            var result = await query.AsQueryable().PageBy(input).ToListAsync();
            var props = new List<GetBabyPropsOutput>();
            if (result != null && result.Any())
            {
                var termIds = new List<int>();
                result.Select(s => s.BabyPropTerms).ToList().ForEach(s =>
                 {
                     termIds.AddRange(s.Select(d => d.Id));
                 });
                var babyPropBuyTerms = _babyPropTermRepository.GetAllIncluding(s => s.BabyPropBuyTerm).Where(s => termIds.Contains(s.Id)).AsNoTracking();

                var featureIds = new List<int>();
                result.Select(s => s.BabyPropFeatures).ToList().ForEach(s =>
               {
                   featureIds.AddRange(s.Select(d => d.Id));
               });
                var babyPropFeatures = _babyPropFeatureRepository.GetAllIncluding(s => s.BabyPropFeatureType).Where(s => featureIds.Contains(s.Id)).AsNoTracking();
                foreach (var item in result)
                {
                    var prop = new GetBabyPropsOutput();
                    prop.BasicInfo = ObjectMapper.Map<GetBabyPropsOutputBasicInfo>(item);
                    //条件
                    var terms = new List<GetBabyPropsOutputDetailTerm>();
                    var babyPropTermIds = item.BabyPropTerms.Select(s => s.Id);
                    await babyPropBuyTerms
                        .Where(s => babyPropTermIds.Contains(s.Id))
                        .ForEachAsync(s =>
                     {
                         terms.Add(ObjectMapper.Map<GetBabyPropsOutputDetailTerm>(s));
                     });
                    //特性
                    var features = new List<GetBabyPropsOutputDetailFeature>();
                    var ItemFeatureIds = item.BabyPropFeatures.Select(s => s.Id);
                    await babyPropFeatures
                      .Where(s => ItemFeatureIds.Contains(s.Id))
                      .ForEachAsync(s =>
                      {
                          features.Add(ObjectMapper.Map<GetBabyPropsOutputDetailFeature>(s));
                      });
                    var propertyAddition = ObjectMapper.Map<GetBabyPropsOutputDetailPropertyAddition>(item.BabyPropPropertyAward);
                    // 合成
                    prop.Detail = new GetBabyPropsOutputDetail()
                    {
                        Terms = terms,
                        Features = features,
                        PropertyAddition = propertyAddition,
                    };
                    props.Add(prop);
                }
            }
            var response = new PagedResultDto<GetBabyPropsOutput>(propCount, props);
            return response;
        }
        /// <summary>
        /// 购买道具
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<PostBuyPropOutput> PostBuyPropAsync(PostBuyPropInput input)
        {
            var response = new PostBuyPropOutput() { IsPopupChangeEquipment = true };
            var prop = (await _entityRepository.GetAllIncluding(s => s.Prices)
                .Include(s => s.BabyPropPropertyAward)
                .Include(t => t.BabyPropType)
                .FirstOrDefaultAsync(s => s.Id == input.PropId))
                .CheckNull($"道具不存在！{input.PropId}");
            var price = prop.Prices.FirstOrDefault(s => s.Id == input.PriceId).CheckNull($"道具不存在价格！{input.PriceId}");
            //判断是否已经买了该道具且没有到期（包含永久），且超过最大持有数量
            var propCount = await _babyFamilyAssetRepository.GetAllIncluding().CountAsync(s => s.BabyPropId == input.PropId && (s.ExpiredDateTime > DateTime.UtcNow || s.ExpiredDateTime == null) && s.FamilyId == input.FamilyId && s.OwnId == input.BabyId);
            if (prop.MaxPurchasesNumber <= propCount)
            {
                throw new AbpException($"已经拥有该道具，请勿重复购买！{prop.MaxPurchasesNumber}");
            }
            // 判断是否有足够的金币，如果足够，减去家庭金币
            var family = (await _familyRepository.GetAllIncluding(s => s.Babies).FirstOrDefaultAsync(s => s.Id == input.FamilyId)).CheckNull($"当前家庭不存在！{input.FamilyId}");
            family.CheckCondition(family.Deposit >= price.Price, $"当前家庭金币数量不足！deposit：{family.Deposit}，price：{price.Price}");
            //减去金币
            family.Deposit -= price.Price;
            family = await _familyRepository.UpdateAsync(family);

            BabyFamilyAsset familyAsset = await UpdateFamilyAssetAndBabyAward(new UpdateFamilyAssetAndBabyAwardDto()
            {
                BuyProp = input,
                Family = ObjectMapper.Map<UpdateFamilyAssetAndBabyAwardFamily>(family), //family.MapTo<UpdateFamilyAssetAndBabyAwardFamily>(),
                Prop = ObjectMapper.Map<BuyBabyPropDto>(prop),// prop.MapTo<BuyBabyPropDto>(),
                PropPrice = ObjectMapper.Map<BabyPropPriceDto>(price),//price.MapTo<BabyPropPriceDto>()
            });
            //TODO：是否需要跑马灯
            response.AssetId = familyAsset.Id;
            return response;
        }
        [RemoteService(IsEnabled = false)]
        public void BuyFreeProps(int babyId, int familyId, Guid playerId)
        {
            var frees = (from prop in _entityRepository.GetAllIncluding(s => s.Prices) where prop.IsDefault select prop).ToList();
            if (frees.Count == 0) return;
            frees.ForEach((item) =>
            {
                Abp.Threading.AsyncHelper.RunSync(() => this.PostBuyPropAsync(new PostBuyPropInput
                {
                    BabyId = babyId,
                    FamilyId = familyId,
                    PlayerGuid = playerId,
                    PropId = item.Id,
                    PriceId = item.Prices.First().Id
                }));
            });
        }
        /// <summary>
        /// 添加购买道具记录
        /// </summary>
        /// <param name="input"></param>
        /// <param name="price"></param>
        /// <param name="family"></param>
        /// <returns></returns>
        private async Task AddBuyPropRecord(PostBuyPropInput input, BabyPropPriceDto price, UpdateFamilyAssetAndBabyAwardFamily family)
        {
            // 添加购买记录
            var assetRecord = await _babyPropRecordRepository.InsertAsync(new BabyPropRecord()
            {
                BabyPropId = input.PropId,
                FamilyId = input.FamilyId,
                PropSource = PropSource.BuyFromStore,
                PurchaserId = input.PlayerGuid,
                BabyId = input.BabyId
            });
            // 增加对于金币消耗的记录
            var res = await _familyCoinDepositChangeRecordRepository.InsertAsync(new FamilyCoinDepositChangeRecord()
            {
                Amount = price.Price,
                BabyId = input.BabyId,
                FamilyId = input.FamilyId,
                CostType = CoinCostType.BuyProp,
                StakeholderId = input.PlayerGuid,
                CurrentFamilyCoinDeposit = family.Deposit
            });
        }


        [UnitOfWork]
        public async Task<BabyFamilyAsset> UpdateFamilyAssetAndBabyAward(UpdateFamilyAssetAndBabyAwardDto input)
        {
            var record = AddBuyPropRecord(input.BuyProp, input.PropPrice, input.Family);
            //var familyAsset = new BabyFamilyAsset();
            //int? ownId = 0;
            //if (input.Prop.BabyPropType != null && input.Prop.BabyPropType.EquipmentAbleObject == EquipmentAbleObject.Baby)
            //{
            //    ownId = input.BuyProp.BabyId;
            //}
            //else
            //{
            //    ownId = null;
            //}

            // 添加到家庭资产
            var familyAsset = await _babyFamilyAssetRepository.InsertAsync(new BabyFamilyAsset
            {
                BabyPropId = input.BuyProp.PropId,
                FamilyId = input.BuyProp.FamilyId,
                OwnId = input.Family.LatestBaby.Id,
                ExpiredDateTime = input.PropPrice.Validity == -1 ? null : (DateTime?)DateTime.UtcNow.AddSeconds(Convert.ToDouble((input.PropPrice?.Validity))),
                BabyPropPriceId = input.PropPrice.Id
            });

            // 增加/更新家庭装备附加特性
            var assetAwardId = await _babyAssetAwardRepository.InsertAndGetIdAsync(new BabyAssetAward()
            {
                BabyFamilyAsset = familyAsset,
                FamilyId = input.BuyProp.FamilyId,
                BabyId = input.BuyProp.BabyId,
                ExpiredDateTime = input.PropPrice.Validity == -1 ? null : (DateTime?)DateTime.UtcNow.AddSeconds(input.PropPrice.Validity),
                Charm = input.Prop.BabyPropPropertyAward.Charm,
                Imagine = input.Prop.BabyPropPropertyAward.Imagine,
                EmotionQuotient = input.Prop.BabyPropPropertyAward.EmotionQuotient,
                Intelligence = input.Prop.BabyPropPropertyAward.Intelligence,
                Physique = input.Prop.BabyPropPropertyAward.Physique,
                WillPower = input.Prop.BabyPropPropertyAward.WillPower,
            });
            // 增加到当前宝宝身上，并且设定定时减去，PS：注意部分可以继承的道具，增加x宝宝的属性
            var baby = input.Family.LatestBaby;
            var propertyAddtion = input.Prop.BabyPropPropertyAward;
            if (propertyAddtion != null && input.Prop.BabyPropPropertyAward.EventAdditionType == EventAdditionType.PropAddititon)
            {
                baby.Charm += propertyAddtion.Charm;
                baby.EmotionQuotient += propertyAddtion.EmotionQuotient;
                baby.Imagine += propertyAddtion.Imagine;
                baby.Intelligence += propertyAddtion.Intelligence;
                baby.Physique += propertyAddtion.Physique;
                baby.WillPower += propertyAddtion.WillPower;
                await _babyRepository.UpdateAsync(baby);
            }
            // 永久的不用定时处理
            if (input.PropPrice.Validity != -1)
            {
                var timeSpan = new DateTimeOffset(DateTime.Now.AddSeconds(input.PropPrice.Validity));
                BackgroundJob.Schedule<BabyPropAppService>((s) => s.HandleUpdateBabyPropertyJob(assetAwardId, input.BuyProp.BabyId, input.BuyProp.PropId, input.BuyProp.FamilyId), timeSpan);
            }


            return familyAsset;
        }

        [UnitOfWork]
        public virtual async Task HandleUpdateBabyPropertyJob(Guid assetAwardId, int babyId, int propId, int familyId)
        {
            // 减去宝宝属性
            var assetAward = await _babyAssetAwardRepository.GetAllIncluding(s => s.Baby).FirstOrDefaultAsync(s => s.Id == assetAwardId);
            if (assetAward != null)
            {
                if (assetAward.Baby != null)
                {
                    var baby = assetAward.Baby;
                    baby.Charm -= assetAward.Charm;
                    baby.EmotionQuotient -= assetAward.EmotionQuotient;
                    baby.Imagine -= assetAward.Imagine;
                    baby.Intelligence -= assetAward.Intelligence;
                    baby.Physique -= assetAward.Physique;
                    baby.WillPower -= assetAward.WillPower;
                    await _babyRepository.UpdateAsync(baby);
                }

                // 修改装备奖励表状态
                assetAward.State = BabyAssetAwardState.Handled;
                // 重新计算家庭装备特性
                var res = await _babyFamilyAssetAppService.ReCalculateAssetFeatureAddition(new ReCalculateAssetFeatureAdditionInput
                {
                    BabyId = babyId,
                    FamilyId = assetAward.FamilyId,
                    AssetId = null,
                    PropId = propId,
                });

                await _babyAssetAwardRepository.UpdateAsync(assetAward);
            }


            //TODO: 添加过期消息
            await _babyAssetAwardRepository.UpdateAsync(assetAward);
            var _infoRepository = IocManager.Instance.Resolve<IRepository<Information, Guid>>();
            var _babyPropRepository = IocManager.Instance.Resolve<IRepository<BabyProp>>();
            var _current_babyprop = await _babyPropRepository.FirstOrDefaultAsync(p => p.Id == propId);
            var _current_family = await _familyRepository.FirstOrDefaultAsync(f => f.Id == familyId);
            await Task.WhenAll(new Task[] {
                 _infoRepository.InsertAsync(new Information
            {
                Content = $"您的{_current_babyprop.Title}道具已过期，若想继续使用，请到商城购买",
                FamilyId = familyId,
                Type = InformationType.System,
                SenderId = null
            }),
            //     _infoRepository.InsertAsync(new Information
            //{
            //    Content = $"您的{_current_babyprop.Title}道具已过期，若想继续使用，请到商城购买",
            //    FamilyId = familyId,
            //    State = InformationState.Create,
            //    Type = InformationType.System,
            //    ReceiverId = _current_family.MotherId,
            //    SenderId = _current_family.FatherId,
            //    SystemInformationType = SystemInformationType.Default,
            //    BabyEventId = null,
            //    Remark = null
            //})

        });
        }
        /// <summary>
        /// 发奖给竞技场玩家
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<AwardPrizeToArenaPlayerOutput> AwardPrizeToArenaPlayer(AwardPrizeToArenaPlayerInput input)
        {
            var response = new AwardPrizeToArenaPlayerOutput() { };
            var prop = (await _entityRepository.GetAllIncluding(s => s.Prices).Include(s => s.BabyPropPropertyAward).FirstOrDefaultAsync(s => s.Id == input.PropId)).CheckNull($"道具不存在！{input.PropId}");
            var price = prop.Prices.FirstOrDefault(s => s.Id == input.PriceId).CheckNull($"道具不存在价格！{input.PriceId}");
            //判断是否已经买了该道具且没有到期，且超过最大持有数量
            var propCount = await _babyFamilyAssetRepository.GetAllIncluding().CountAsync(s => s.BabyPropId == input.PropId && (s.ExpiredDateTime > DateTime.UtcNow || s.ExpiredDateTime == null));
            if (prop.MaxPurchasesNumber <= propCount)
            {
                // TODO：如果已经有该道具了，则折成金币发放
                var family = (await _familyRepository.GetAsync(input.FamilyId)).CheckNull($"当前家庭不存在！{input.FamilyId}");
                // 增加金币
                family.Deposit += price.PropValue;
                family = await _familyRepository.UpdateAsync(family);
                response.Message = $"已经有该道具了，折成金币发放，金币数量为：{price.PropValue}";
                //// 增加对于金币消耗的记录
                await _familyCoinDepositChangeRecordRepository.InsertAsync(new FamilyCoinDepositChangeRecord()
                {
                    Amount = price.Price,
                    BabyId = input.BabyId,
                    FamilyId = input.FamilyId,
                    GetWay = CoinGetWay.PropToCoin,
                    //StakeholderId = input.PlayerGuid,
                    CurrentFamilyCoinDeposit = family.Deposit
                });
            }
            else
            {
                // 添加到家庭资产
                var familyAsset = await _babyFamilyAssetRepository.InsertAsync(new BabyFamilyAsset
                {
                    BabyPropId = input.PropId,
                    FamilyId = input.FamilyId,
                    ExpiredDateTime = price.Validity == -1 ? null : (DateTime?)DateTime.UtcNow.AddSeconds(Convert.ToDouble((price?.Validity))),
                    BabyPropPriceId = input.PriceId,
                });
                // 增加家庭装备附加特性
                await _babyAssetAwardRepository.InsertAsync(new BabyAssetAward()
                {
                    BabyFamilyAsset = familyAsset,
                    FamilyId = input.FamilyId,
                    ExpiredDateTime = DateTime.UtcNow.AddSeconds(price.Validity)
                });
                // TODO:随后添加记录

                // 添加购买记录
                var assetRecord = await _babyPropRecordRepository.InsertAsync(new BabyPropRecord()
                {
                    BabyPropId = input.PropId,
                    FamilyId = input.FamilyId,
                    PropSource = PropSource.PresentByArena,
                    //PurchaserId = input.PlayerGuid
                });
            }
            //TODO：是否需要跑马灯

            return response;
        }

        internal override IQueryable<BabyProp> GetQuery(GetBabyPropsInput model)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取购买条件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public ICollection<GetFamilyPropBuyInfoOutputPropTerm> GetPropBuyTerms(PropStaffBuyTermInput input)
        {
            var itemTerms = new List<GetFamilyPropBuyInfoOutputPropTerm>();
            input.BabyProp.BabyPropTerms.ToList().ForEach(d =>
           {
               // 条件类型
               var termType = input.BabyPropBuyTermTypes.FirstOrDefault(o => o.Id == d.BabyPropBuyTermId);
               // 获取条件
               var termStaff = _babyPropBuyTermApplicationService.ValideBabyPropTermSatisfyHandle(new BabyPropBuyTermIsSatisfyInput() { FamilyId = input.FamilyId, Term = d, BabyId = input.BabyId, BabyProp = input.BabyProp, BabyPropBuyTermType = termType }).Result;
               var term = new GetFamilyPropBuyInfoOutputPropTerm() { Id = d.Id, IsSatisfy = termStaff, Title = termType.Title };
               itemTerms.Add(term);
           });
            return itemTerms;
        }
    }


}


