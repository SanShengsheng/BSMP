
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


using MQKJ.BSMP.ChineseBabies.Backpack;
using MQKJ.BSMP.ChineseBabies.Backpack.Dtos;
using MQKJ.BSMP.ChineseBabies.PropMall;
using MQKJ.BSMP.Utils.Extensions;
using Newtonsoft.Json;
using MQKJ.BSMP.ChineseBabies.Asset;
using Abp.Domain.Uow;
using Abp.BackgroundJobs;
using Hangfire;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Backpack
{
    /// <summary>
    /// BabyFamilyAsset应用层服务的接口实现方法  
    ///</summary>
    public class BabyFamilyAssetAppService : BsmpApplicationServiceBase<BabyFamilyAsset, Guid, BabyFamilyAssetEditDto, BabyFamilyAssetEditDto, GetBabyFamilyAssetsInput, BabyFamilyAssetListDto>, IBabyFamilyAssetAppService
    {
        private readonly IRepository<BabyFamilyAsset, Guid> _entityRepository;
        private readonly IRepository<BabyPropTerm, int> _babyPropTermRepository;
        private readonly IRepository<BabyPropFeature, int> _babyPropFeatureRepository;
        private readonly IRepository<BabyAssetRecord, Guid> _babyAssetRecordRepository;
        private readonly IRepository<BabyAssetFeature, Guid> _babyAssetFeatureRepository;
        private readonly IRepository<BabyAssetFeatureRecord, Guid> _babyAssetFeatureRecordRepository;
        private readonly IRepository<Family, int> _familyRepository;
        private readonly IRepository<BabyEventRecord, Guid> _babyEventRecordRepository;
        //private readonly IRepository<BabyEventOption, int> _babyEventOptionRepository;
        private readonly IRepository<BabyAssetAward, Guid> _babyAssetAwardRepository;
        private readonly IRepository<Baby, int> _babyRepository;
        private readonly IRepository<BabyGrowUpRecord, Guid> _babyGrowUpRecordRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public BabyFamilyAssetAppService(
        IRepository<BabyFamilyAsset, Guid> entityRepository,
        IRepository<BabyAssetRecord, Guid> babyAssetRecordRepository,
        IRepository<BabyAssetFeature, Guid> babyAssetFeatureRepository,
                IRepository<BabyAssetFeatureRecord, Guid> babyAssetFeatureRecordRepository,
                IRepository<Family, int> familyRepository,
                 IRepository<BabyEventRecord, Guid> babyEventRecordRepository,
                   IRepository<BabyPropTerm, int> babyPropTermRepository,
        IRepository<BabyPropFeature, int> babyPropFeatureRepository,
        IRepository<Baby, int> babyRepository,
        IRepository<BabyAssetAward, Guid> babyAssetAwardRepository,
         IRepository<BabyGrowUpRecord, Guid> babyGrowUpRecordRepository
        ) : base(entityRepository)
        {
            _entityRepository = entityRepository;
            _babyAssetFeatureRecordRepository = babyAssetFeatureRecordRepository;
            _babyAssetFeatureRepository = babyAssetFeatureRepository;
            _babyAssetRecordRepository = babyAssetRecordRepository;
            _familyRepository = familyRepository;
            _babyEventRecordRepository = babyEventRecordRepository;
            _babyPropTermRepository = babyPropTermRepository;
            _babyPropFeatureRepository = babyPropFeatureRepository;
            _babyRepository = babyRepository;
            _babyAssetAwardRepository = babyAssetAwardRepository;
            _babyGrowUpRecordRepository = babyGrowUpRecordRepository;
        }

        public async Task<GetBabyFamilyAssetByPageOutput> GetPage(GetBabyFamilyAssetsInput input)
        {
            var response = new GetBabyFamilyAssetByPageOutput();
            //获取家庭基本信息
            var family = await _familyRepository.GetAsync(input.FamilyId);
            response.FamilyBasicInfo = ObjectMapper.Map<GetBabyFamilyAssetByPageOutput_FamilyBasicInfo>(family);
            if (input.PropTypeId == 0)
            {
                response.EventAssets = await GetEventGoodsByPage(input);
            }
            else
            {
                response.Assets = await GetFamilyAssetByPage(input);
            }
            return response;
        }
        /// <summary>
        /// 获取事件类型的资产
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<PagedResultDto<GetBabyFamilyAssetByPageOutputEventAsset>> GetEventGoodsByPage(GetBabyFamilyAssetsInput input)
        {
            var eventGoods = new List<GetBabyFamilyAssetByPageOutputEventAsset>();
            var query = _babyEventRecordRepository.GetAllIncluding()
             .Include(s => s.Event).ThenInclude(s => s.Options)
             .Where(s => s.FamilyId == input.FamilyId && s.BabyId == input.BabyId && s.Event.Type == IncidentType.Growup && s.State == EventRecordState.Handled)
             .Select(s => s.Event.Options.FirstOrDefault(d => d.Id == s.OptionId)).Where(opts => opts.IsProp)
             .OrderBy(input.Sorting)
             .AsNoTracking();

            var eventGoodCount = await query.CountAsync();
            if (input.PageSize > 0)
            {
                input.MaxResultCount = input.PageSize;
            }
            if (input.PageIndex > 0)
            {
                input.SkipCount = (input.PageIndex - 1) * input.PageSize;
            }
            var result = await query.PageBy(input).ToListAsync();
            result.ForEach(d =>
            {
                eventGoods.Add(new GetBabyFamilyAssetByPageOutputEventAsset() { Title = d.Content });
            });
            return new PagedResultDto<GetBabyFamilyAssetByPageOutputEventAsset>(eventGoodCount, eventGoods);
        }
        /// <summary>
        /// 获取家庭资产（从商城获得）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<PagedResultDto<GetBabyFamilyAssetByPageOutputAsset>> GetFamilyAssetByPage(GetBabyFamilyAssetsInput input)
        {
            var query = _entityRepository.GetAllIncluding()
                .Include(s => s.BabyProp).ThenInclude(s => s.Prices)
                .Include(s => s.BabyProp).ThenInclude(s => s.BabyPropType)
                .Include(s => s.BabyProp).ThenInclude(s => s.BabyPropTerms)
                .Include(s => s.BabyProp).ThenInclude(s => s.BabyPropFeatures)
                .Include(s => s.BabyProp).ThenInclude(s => s.BabyPropPropertyAward)
                .Where(s => (s.FamilyId == input.FamilyId && s.BabyProp.BabyPropTypeId == input.PropTypeId
              && (s.ExpiredDateTime > DateTime.UtcNow || s.ExpiredDateTime == null) && s.BabyProp.IsInheritAble)
              || (s.FamilyId == input.FamilyId && s.BabyProp.BabyPropTypeId == input.PropTypeId
              && (s.ExpiredDateTime > DateTime.UtcNow || s.ExpiredDateTime == null) && !s.BabyProp.IsInheritAble && s.OwnId == input.BabyId)
              )
                .WhereIf(input.Gender.HasValue, p => p.BabyProp.Gender == input.Gender || p.BabyProp.Gender == Gender.All)
                .AsNoTracking();

            //TODO:已经购买的排在队列最后
            var propCount = await query.AsQueryable().CountAsync();
            if (input.PageSize > 0)
            {
                input.MaxResultCount = input.PageSize;
            }
            if (input.PageIndex > 0)
            {
                input.SkipCount = (input.PageIndex - 1) * input.PageSize;
            }
            var result = await query.AsQueryable().PageBy(input).ToListAsync();
            var props = new List<GetBabyFamilyAssetByPageOutputAsset>();
            if (result != null && result.Any())
            {
                var termIds = new List<int>();
                result.Select(s => s.BabyProp.BabyPropTerms).ToList().ForEach(s =>
                {
                    termIds.AddRange(s.Select(d => d.Id));
                });
                var babyPropBuyTerms = _babyPropTermRepository.GetAllIncluding(s => s.BabyPropBuyTerm).Where(s => termIds.Contains(s.Id)).AsNoTracking();

                var featureIds = new List<int>();
                result.Select(s => s.BabyProp.BabyPropFeatures).ToList().ForEach(s =>
                {
                    featureIds.AddRange(s.Select(d => d.Id));
                });
                var babyPropFeatures = _babyPropFeatureRepository.GetAllIncluding(s => s.BabyPropFeatureType).Where(s => featureIds.Contains(s.Id)).AsNoTracking();
                foreach (var asset in result)
                {
                    var item = asset.BabyProp;
                    var prop = new GetBabyFamilyAssetByPageOutputAsset();
                    var propOutInfo = ObjectMapper.Map<GetBabyFamilyAssetByPageOutputDetailPropInfo>(item);
                    prop.BasicInfo = ObjectMapper.Map<GetBabyFamilyAssetByPageOutputBasicInfo>(asset);

                    //条件
                    var terms = new List<GetBabyFamilyAssetByPageOutputDetailTerm>();
                    var babyPropTermIds = item.BabyPropTerms.Select(s => s.Id);
                    await babyPropBuyTerms
                        .Where(s => babyPropTermIds.Contains(s.Id))
                        .ForEachAsync(s =>
                        {
                            terms.Add(ObjectMapper.Map<GetBabyFamilyAssetByPageOutputDetailTerm>(s));
                        });
                    //特性
                    var features = new List<GetBabyFamilyAssetByPageOutputDetailFeature>();
                    var ItemFeatureIds = item.BabyPropFeatures.Select(s => s.Id);
                    await babyPropFeatures
                      .Where(s => ItemFeatureIds.Contains(s.Id))
                      .ForEachAsync(s =>
                      {
                          features.Add(ObjectMapper.Map<GetBabyFamilyAssetByPageOutputDetailFeature>(s));
                      });
                    var propertyAddition = ObjectMapper.Map<GetBabyFamilyAssetByPageOutputDetailPropertyAddition>(item.BabyPropPropertyAward);
                    // 合成
                    prop.Detail = new GetBabyFamilyAssetByPageOutputDetail()
                    {
                        Terms = terms,
                        Features = features,
                        PropInfo = propOutInfo,
                        PropertyAddition = propertyAddition,
                    };
                    props.Add(prop);
                }
            }
            var response = new PagedResultDto<GetBabyFamilyAssetByPageOutputAsset>(propCount, props);
            return response;
        }

        /// <summary>
        /// 更换装备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<PostChangeAssetOutput> PostChangeAsset(PostChangeAssetInput input)
        {
            var response = new PostChangeAssetOutput();
            var asset = await _entityRepository.GetAllIncluding()
               .Include(s => s.BabyProp).ThenInclude(s => s.BabyPropType)
                .Include(s => s.BabyProp).ThenInclude(s => s.BabyPropFeatures).ThenInclude(s => s.BabyPropFeatureType)
               .FirstOrDefaultAsync(s => s.Id == input.AssetId).CheckNull($"装备不存在！{input.AssetId}");
            // TODO:
            // 判断是否重复装备某一物品（同样的物品尚未失效，一般来说不存在这种情况）

            // 判断道具是否为装备状态
            asset.CheckCondition(!asset.IsEquipmenting, $"装备被占用！{input.AssetId}");
            // 判断道具是否已失效
            asset.CheckCondition(asset.ExpiredDateTime == null || asset.ExpiredDateTime > DateTime.UtcNow, $"装备已过期！ExpiredDateTime：{asset.ExpiredDateTime}，UtcNow：{DateTime.UtcNow}");
            // 更新装备
            var oldAssetIds = await UpdateAsset(input, asset);
            // 更新装备特性加成
            var assetFeatureProperty = await ReCalculateAssetFeatureAddition(new ReCalculateAssetFeatureAdditionInput()
            {
                OldAssetIds = oldAssetIds,
                AssetId = input.AssetId,
                BabyId = input.BabyId,
                FamilyId = input.FamilyId,
                PropId = asset.BabyPropId,
            });
            response.LastAssetFeatureProperty = assetFeatureProperty;
            //TODO 宝宝成人后需要将不能继承的道具 处理下
            return response;
        }
        /// <summary>
        /// 更新装备
        /// </summary>
        /// <param name="input"></param>
        /// <param name="asset"></param>
        /// <returns></returns>
        private async Task<List<Guid>> UpdateAsset(PostChangeAssetInput input, BabyFamilyAsset asset)
        {
            var res = new List<Guid>();
            // 修改装备状态为装备中
            asset.IsEquipmenting = true;
            if (asset.BabyProp.BabyPropType.EquipmentAbleObject == EquipmentAbleObject.Baby)
            {
                asset.OwnId = input.BabyId;
            }
            await _entityRepository.InsertOrUpdateAsync(asset);
            // 记录装备操作记录
            await _babyAssetRecordRepository.InsertAsync(new BabyAssetRecord()
            {
                FamilyId = input.FamilyId,
                BabyId = input.BabyId,
                FamilyAssetId = input.AssetId,
                EquipmentState = EquipmentState.Equipment
            });
            // TODO: 此处周一优化，与AddAssetFeatureAddition函数可进行重构
            //var babyAssetFeature = await _babyAssetFeatureRepository.FirstOrDefaultAsync(a => a.BabyId == input.BabyId && a.FamilyId == input.FamilyId);
            // 将刚脱掉的装备状态置为未装备
            // 1. 当前家庭装备加成
            //var assetAddition = await _babyAssetFeatureRepository
            //   .FirstOrDefaultAsync(s => s.BabyId == input.BabyId && s.FamilyId == input.FamilyId);
            //// 2. 反序列化装备特性属性
            //var assetAdditionProperties = assetAddition == null ? new List<BabyAssetFeatureProperty>() : JsonConvert.DeserializeObject<List<BabyAssetFeatureProperty>>(assetAddition.AssetFeatureProperty);
            //var assetFeaturePropertyBefore = assetAddition?.AssetFeatureProperty;

            //var assetProperties = new List<object>();

            await _entityRepository.GetAll().Include(s => s.BabyProp).ThenInclude(s => s.BabyPropFeatures)
               .Where(s => s.BabyProp.BabyPropTypeId == asset.BabyProp.BabyPropTypeId && s.FamilyId == input.FamilyId && s.Id != input.AssetId && s.IsEquipmenting).ForEachAsync(async s =>
                  {
                      res.Add(s.Id);
                      s.IsEquipmenting = false;
                      await _entityRepository.InsertOrUpdateAsync(s);
                      // 记录装备操作记录
                      // 脱掉同类型的其他装备（默认同类型道具同时只能装备一个，表里有配置该字段），比如装备A房子后，脱掉原先的B房子
                      await _babyAssetRecordRepository.InsertAsync(new BabyAssetRecord()
                      {
                          FamilyId = input.FamilyId,
                          BabyId = input.BabyId,
                          FamilyAssetId = s.Id,
                          EquipmentState = EquipmentState.UnEquipment
                      });

                  });
            return res;

        }

        /// <summary>
        /// 重新计算装备加成
        /// </summary>
        /// <param name="input"></param>
        /// <param name="propId"></param>
        /// <param name="oldAssetIds">被移除的装备编号</param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<string> ReCalculateAssetFeatureAddition(ReCalculateAssetFeatureAdditionInput input)
        {
            var assetAddition = await _babyAssetFeatureRepository
                .FirstOrDefaultAsync(s => s.BabyId == input.BabyId && s.FamilyId == input.FamilyId);
            //var assetAdditionProperties = assetAddition == null ? new List<BabyAssetFeatureProperty>() : JsonConvert.DeserializeObject<List<BabyAssetFeatureProperty>>(assetAddition.AssetFeatureProperty);
            var assetFeaturePropertyBefore = assetAddition?.AssetFeatureProperty;
            var assetProperties = new List<BabyAssetFeatureProperty>();
            //  重新计算装备特性加成
            await _entityRepository.GetAllIncluding()
                    .Include(s => s.BabyProp).ThenInclude(s => s.BabyPropFeatures).ThenInclude(s => s.BabyPropFeatureType)
                    .Where(s => s.FamilyId == input.FamilyId && (s.IsEquipmenting || s.Id == input.AssetId) && (s.ExpiredDateTime > DateTime.UtcNow || s.ExpiredDateTime == null))
                    .WhereIf(input.IsInheritRequest, s => s.OwnId != input.BabyId && s.BabyProp.IsInheritAble)
                    .WhereIf(input.OldAssetIds?.Count > 0, s => !input.OldAssetIds.Contains(s.Id))
                    .ForEachAsync(f =>
                    {
                        f.BabyProp.BabyPropFeatures.Where(s => true).ToList().ForEach(d =>
                         {
                             var assetProperty = new BabyAssetFeatureProperty() { FeatureTypeId = d.BabyPropFeatureTypeId, Name = d.BabyPropFeatureType.Name, Value = d.Value, Title = d.BabyPropFeatureType.Title };
                             if (assetAddition != null)
                             {
                                 // 计算最新的特性属性加成
                                 var addition = assetProperties.FirstOrDefault(s => s?.Name == d.BabyPropFeatureType.Name);
                                 if (addition != null)
                                 {
                                     assetProperties.Remove(addition);
                                     assetProperty.Value = d.Value + addition.Value;
                                 }

                             }
                             assetProperties.Add(assetProperty);
                         });
                    });
            if (assetAddition == null)
            {
                assetAddition = new BabyAssetFeature()
                {
                    FamilyId = input.FamilyId,
                    BabyId = input.BabyId,
                    AssetFeatureProperty = JsonConvert.SerializeObject(assetProperties)
                };
            }

            assetAddition.AssetFeatureProperty = JsonConvert.SerializeObject(assetProperties);
            if (assetProperties != null && assetProperties.Count > 0 && assetAddition.AssetFeatureProperty != "\"[]\"")
            {
                assetAddition = await _babyAssetFeatureRepository.InsertOrUpdateAsync(assetAddition);
                // 记录装备特性
                await _babyAssetFeatureRecordRepository.InsertAsync(new BabyAssetFeatureRecord()
                {
                    BabyId = input.BabyId,
                    FamilyId = input.FamilyId,
                    AssetFeatureProperty = assetFeaturePropertyBefore,
                    LastAssetFeatureProperty = assetAddition.AssetFeatureProperty,
                    BabyPropId = input.PropId,
                });
            }

            return assetAddition.AssetFeatureProperty;

        }
        /// <summary>
        /// 继承家庭装备的属性加成（新出生宝宝）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task InheritFamilyAssetPropertyAddion(InheritFamilyAssetPropertyAddionInput input)
        {
            var isUpdateBabyProperty = false;
            var baby = input.Baby;
            var babyBasicProperty = new BabyBasePropertyDto();
            // 循环家庭资产
            await _entityRepository.GetAllIncluding()
                  .Include(s => s.BabyProp).ThenInclude(s => s.BabyPropPropertyAward)
               .Where(s => s.FamilyId == input.FamilyId && (s.ExpiredDateTime == null || s.ExpiredDateTime > DateTime.UtcNow) && s.BabyProp.IsInheritAble).ForEachAsync(async s =>
                 {
                     var award = s.BabyProp.BabyPropPropertyAward;
                     var assetAwardIdAsync = _babyAssetAwardRepository.InsertAndGetIdAsync(new BabyAssetAward()
                     {
                         BabyFamilyAsset = s,
                         FamilyId = input.FamilyId,
                         BabyId = input.BabyId,
                         ExpiredDateTime = s.ExpiredDateTime,
                         Charm = award.Charm,
                         Imagine = award.Imagine,
                         EmotionQuotient = award.EmotionQuotient,
                         Intelligence = award.Intelligence,
                         Physique = award.Physique,
                         WillPower = award.WillPower,
                     });
                     isUpdateBabyProperty = true;
                     // 宝宝属性增加
                     babyBasicProperty.Charm += award.Charm;
                     babyBasicProperty.Imagine += award.Imagine;
                     babyBasicProperty.EmotionQuotient += award.EmotionQuotient;
                     babyBasicProperty.Intelligence += award.Intelligence;
                     babyBasicProperty.Physique += award.Physique;
                     babyBasicProperty.WillPower += award.WillPower;
                     // 针对过期性道具，需要在道具过期后去掉该部分属性加成
                     if (s.ExpiredDateTime != null)
                     {
                         var assetAwardId = await assetAwardIdAsync;
                         var timeSpan = Convert.ToDateTime(s.ExpiredDateTime).ToLocalTime();
                         BackgroundJob.Schedule<BabyPropAppService>((job) => job.HandleUpdateBabyPropertyJob(assetAwardId, input.BabyId, s.BabyPropId, input.FamilyId), timeSpan);
                     }
                     // 将家庭资产的拥有人改为当前宝宝
                     s.OwnId = input.BabyId;
                 });
            // 将属性加成增加到宝宝身上
            if (isUpdateBabyProperty)
            {
                //var updateBabyResultAsync = _babyRepository.UpdateAsync(baby);
                baby.Charm += babyBasicProperty.Charm;
                baby.Imagine += babyBasicProperty.Imagine;
                baby.EmotionQuotient += babyBasicProperty.EmotionQuotient;
                baby.Intelligence += babyBasicProperty.Intelligence;
                baby.Physique += babyBasicProperty.Physique;
                baby.WillPower += babyBasicProperty.WillPower;
                //var updateBabyResultAsync = _babyRepository.UpdateAsync(baby);
                await _babyGrowUpRecordRepository.InsertAsync(new BabyGrowUpRecord()
                {
                    BabyId = baby.Id,
                    CreationTime = DateTime.Now,
                    Charm = babyBasicProperty.Charm,
                    EmotionQuotient = babyBasicProperty.EmotionQuotient,
                    Imagine = babyBasicProperty.Imagine,
                    Intelligence = babyBasicProperty.Intelligence,
                    Physique = babyBasicProperty.Physique,
                    WillPower = babyBasicProperty.WillPower,
                    PlayerId = input.PlayerGuid,
                    TriggerType = TriggerType.InheritFamilyAssetddition,
                });
            }

        }

        internal override IQueryable<BabyFamilyAsset> GetQuery(GetBabyFamilyAssetsInput model)
        {
            throw new NotImplementedException();
        }


    }
}


