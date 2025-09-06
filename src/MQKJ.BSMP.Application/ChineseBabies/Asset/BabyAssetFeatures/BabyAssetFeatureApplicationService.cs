using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies.Backpack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies.Asset.BabyAssetFeatures
{
    public class BabyAssetFeatureApplicationService : IBabyAssetFeatureApplicationService
    {
        private readonly IRepository<BabyAssetFeature, Guid> _entityRepository;
        private readonly IRepository<BabyPropFeatureType, Guid> _babyPropFeatureTypeRepository;

        #region 道具类型
        /**
       
AddExtraGrowUpEventRewardBabyPropertyRatio	额外增加完成主事件获得的属性
AddExtraStudyEventRewardBabyPropertyRatio	额外增加完成学习事件获得的属性
AddExtraGrowUpAndStudyEventRewardBabyPropertyRatio	额外增加完成成长和学习事件获得的属性
ReduceGrowUpEventCD	降低主事件的CD时间
ReduceStudyEventCD	降低学习事件的CD时间
ReduceGrowUpAndStudyEventCD	降低成长和学习事件CD时间
ReduceGrowUpEventCostGoldCoinRatio	降低主事件的消耗金币比例
ReduceStudyEventCostGoldCoinRatio	降低学习事件的消耗金币比例
ReduceGrowUpAndStudyEventCostGoldCoinRatio	降低成长和学习事件消耗金币比例
ReduceGrowUpEventCostEnergyRatio	降低主事件的消耗精力比例
ReduceStudyEventCostEnergyRatio	降低学习事件的消耗精力比例
ReduceGrowUpAndStudyEventCostEnergyRatio	降低成长和学习事件消耗精力比例
IncreaseArenaFightWinProbability	提高竞技场战斗时胜率
      
      * */
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enityRepository"></param>
        public BabyAssetFeatureApplicationService(
              IRepository<BabyAssetFeature, Guid> enityRepository
            , IRepository<BabyPropFeatureType, Guid> babyPropFeatureTypeRepository
            )
        {
            _entityRepository = enityRepository;
            _babyPropFeatureTypeRepository = babyPropFeatureTypeRepository;
        }
        /// <summary>
        /// 获取道具特性
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<AssetFeatureDto> GetAssetFeature(AssetFeatureWorkInput input)
        {
            // 先定目标
            var response = new AssetFeatureDto();
            // 获取家庭特性
            var familyAssetFeature =await _entityRepository.GetAll().FirstOrDefaultAsync(s => s.FamilyId == input.FamilyId && s.BabyId == input.BabyId);
            if (familyAssetFeature == null || familyAssetFeature.AssetFeatureProperty == null || familyAssetFeature.AssetFeatureProperty == "\"[]\"")
            {
                return null;
            }
            var features = JsonConvert.DeserializeObject<List<BabyAssetFeatureProperty>>(familyAssetFeature.AssetFeatureProperty);
            var featureTypeIds = features.Select(s => s.FeatureTypeId);
            // 获取特性类型，循环
            var featureTypes = await _babyPropFeatureTypeRepository.GetAllListAsync(s => featureTypeIds.Contains(s.Id) && (s.EventAdditionType == input.EventType || s.EventAdditionType == EventAdditionType.GrowUpAndStudy));
            response.CD = GetFeatureValue(features, featureTypes, 2);
            response.Coin = GetFeatureValue(features, featureTypes, 3);
            response.Energy = GetFeatureValue(features, featureTypes, 4);
            response.PropertyAddtion = GetFeatureValue(features, featureTypes, 1);
            return response;
        }
        /// <summary>
        /// 获取特性值，如获取=学习事件CD时间的缩减值，需要计算学习事和成长学习类型的特性
        /// </summary>
        /// <param name="features"></param>
        /// <param name="featureTypes"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        private static double GetFeatureValue(List<BabyAssetFeatureProperty> features, List<BabyPropFeatureType> featureTypes, int group)
        {
            var properyType = featureTypes.Where(f => f.Group == group).Select(s => s.Id);
            var result = features.Where(d => properyType.Contains(d.FeatureTypeId)).Sum(d => d.Value);
            return Math.Round(result, 2);
        }
    }
}
