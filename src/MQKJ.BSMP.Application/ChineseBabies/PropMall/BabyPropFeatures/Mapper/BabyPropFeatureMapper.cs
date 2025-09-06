
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Backpack;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.PropMall;
using System;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

    /// <summary>
    /// 配置BabyPropFeatureMapper的AutoMapper
    /// </summary>
    internal class BabyPropFeatureMapper : Profile
    {
        public BabyPropFeatureMapper()
        {
            // 道具列表
            CreateMap<BabyPropFeature, GetBabyPropsOutputDetailFeature>().ForMember(d => d.Title, s => s.MapFrom(f => f.BabyPropFeatureType.Title + $"{Math.Round(f.Value,2)*100}%"));
            // 家庭资产列表
            CreateMap<BabyPropFeature, GetBabyFamilyAssetByPageOutputDetailFeature>().ForMember(d => d.Title, s => s.MapFrom(f => f.BabyPropFeatureType.Title + $"{Math.Round(f.Value, 2) * 100}%"));

        }
    }
}
