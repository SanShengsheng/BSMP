
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Backpack;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.PropMall;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

    /// <summary>
    /// 配置BabyPropTermMapper的AutoMapper
    /// </summary>
    internal class BabyPropTermMapper : Profile
    {
        public BabyPropTermMapper()
        {
            // 商店道具详情
            CreateMap<BabyPropTerm, GetBabyPropsOutputDetailTerm>().ForMember(d => d.Title, s => s.MapFrom(f => f.BabyPropBuyTerm.Title + (f.MinValue.HasValue ? $"大于{f.MinValue}":"") + (f.MaxValue.HasValue ? $"小于{f.MaxValue}":"")));
            // 家庭资产
            CreateMap<BabyPropTerm, GetBabyFamilyAssetByPageOutputDetailTerm>().ForMember(d => d.Title, s => s.MapFrom(f => f.BabyPropBuyTerm.Title + (f.MinValue.HasValue ? $"大于{f.MinValue}" : "") + (f.MaxValue.HasValue ? $"小于{f.MaxValue}" : "")));

        }
    }
}
