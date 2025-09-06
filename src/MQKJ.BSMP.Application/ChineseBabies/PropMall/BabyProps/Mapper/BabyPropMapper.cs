
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Backpack;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置BabyProp的AutoMapper
    /// </summary>
	internal  class BabyPropMapper:Profile
    {
        public BabyPropMapper()
        {
            CreateMap <BabyProp,BabyPropListDto>();
            CreateMap <BabyPropListDto,BabyProp>();

            CreateMap <BabyPropEditDto,BabyProp>();
            CreateMap <BabyProp,BabyPropEditDto>();

            CreateMap<BabyProp, GetBabyPropsOutputBasicInfo>();
            CreateMap<BabyProp, GetBabyFamilyAssetByPageOutputDetailPropInfo>().ForMember(s=>s.EquipmentAbleObject,d=>d.MapFrom(o=>o.BabyPropType.EquipmentAbleObject));
        }
	}
}
