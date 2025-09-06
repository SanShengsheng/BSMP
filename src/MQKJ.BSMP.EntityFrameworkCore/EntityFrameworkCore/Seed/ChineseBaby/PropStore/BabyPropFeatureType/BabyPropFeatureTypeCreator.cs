using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Backpack;
using System.Collections.Generic;
using System.Linq;
namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class BabyPropFeatureTypeCreator
    {

        private readonly BSMPDbContext _context;

        public BabyPropFeatureTypeCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateBabyPropFeatureType();
        }

        private void CreateBabyPropFeatureType()
        {
            if (_context.BabyPropFeatureTypes.Any())
            {
                return;
            }
            var babyPropFeatureTypes = new List<BabyPropFeatureType>() {
                new BabyPropFeatureType (){
                     Code=1,
                        EventAdditionType = EventAdditionType.GrowUp,
                         Title="额外增加完成主事件获得的属性",
                          Type= FeatureType.BabyProperty,
                          Group=1,
                         Name="AddExtraGrowUpEventRewardBabyPropertyRatio",
                          IsAddition=true
                }
             , new BabyPropFeatureType()
            {
                Code = 2,
                EventAdditionType = EventAdditionType.Study,
                Title = "额外增加完成学习事件获得的属性",
                Type = FeatureType.BabyProperty,
                          Group=1,
                           Name="AddExtraStudyEventRewardBabyPropertyRatio",
                          IsAddition=true,
            }
                , new BabyPropFeatureType()
            {
                Code = 3,
                EventAdditionType = EventAdditionType.GrowUpAndStudy,
                Title = "额外增加完成成长和学习事件获得的属性",
                Type = FeatureType.BabyProperty,
                          Group=1,
Name="AddExtraGrowUpAndStudyEventRewardBabyPropertyRatio",
                          IsAddition=true,
            }
             , new BabyPropFeatureType()
            {
                Code = 4,
                EventAdditionType = EventAdditionType.GrowUp,
                Title = "降低主事件的CD时间",
                Type = FeatureType.Function,
                          Group=2,
                          Name="ReduceGrowUpEventCD"
            }
             , new BabyPropFeatureType()
            {
                Code = 5,
                EventAdditionType = EventAdditionType.Study,
                Title = "降低学习事件的CD时间",
                Type = FeatureType.Function,
                          Group=2,
                           Name="ReduceStudyEventCD"
            }
                 , new BabyPropFeatureType()
            {
                Code = 6,
                EventAdditionType = EventAdditionType.GrowUpAndStudy,
                Title = "降低成长和学习事件CD时间",
                Type = FeatureType.Function,
                          Group=2,
                           Name="ReduceGrowUpAndStudyEventCD"
            }
             , new BabyPropFeatureType()
            {
                Code = 7,
                EventAdditionType = EventAdditionType.GrowUp,
                Title = "降低主事件的消耗金币比例",
                Type = FeatureType.Function,
                          Group=3,
                            Name="ReduceGrowUpEventCostGoldCoinRatio"
            }
             , new BabyPropFeatureType()
            {
                Code = 8,
                EventAdditionType = EventAdditionType.Study,
               Title = "降低学习事件的消耗金币比例",
                Type = FeatureType.Function,
                          Group=3,
                               Name="ReduceStudyEventCostGoldCoinRatio"
            }
                    , new BabyPropFeatureType()
            {
                Code = 9,
                EventAdditionType = EventAdditionType.GrowUpAndStudy,
                Title = "降低成长和学习事件消耗金币比例",
                Type = FeatureType.Function,
                          Group=3,
                           Name="ReduceGrowUpAndStudyEventCostGoldCoinRatio"
            }
             , new BabyPropFeatureType()
            {
                Code = 10,
                EventAdditionType = EventAdditionType.GrowUp,
                   Title = "降低主事件的消耗精力比例",
                Type = FeatureType.BabyProperty,
                          Group=4,
                               Name="ReduceGrowUpEventCostEnergyRatio"
            }
                , new BabyPropFeatureType()
            {
                Code = 11,
                EventAdditionType = EventAdditionType.Study,
                Title = "降低学习事件的消耗精力比例",
                Type = FeatureType.BabyProperty,
                          Group=4,
                          Name="ReduceStudyEventCostEnergyRatio"
            }
                    , new BabyPropFeatureType()
            {
                Code = 12,
                EventAdditionType = EventAdditionType.GrowUpAndStudy,
                Title = "降低成长和学习事件消耗精力比例",
                Type = FeatureType.Function,
                          Group=4,
                           Name="ReduceGrowUpAndStudyEventCostEnergyRatio"
            }
                  , new BabyPropFeatureType()
            {
                Code = 13,
                EventAdditionType = EventAdditionType.Arena,
                Title = "提高竞技场战斗时胜率",
                Type = FeatureType.Function,
                          Group=5,
                               Name="IncreaseArenaFightWinProbability"
            }
            };
            _context.BabyPropFeatureTypes.AddRange(babyPropFeatureTypes);
        }
    }
}
