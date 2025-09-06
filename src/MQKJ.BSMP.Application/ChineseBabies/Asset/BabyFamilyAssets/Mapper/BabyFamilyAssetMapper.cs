
using AutoMapper;
using MQKJ.BSMP.ChineseBabies.Backpack;
using MQKJ.BSMP.ChineseBabies.Backpack.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Backpack.Mapper
{

    /// <summary>
    /// 配置BabyFamilyAsset的AutoMapper
    /// </summary>
    internal class BabyFamilyAssetMapper : Profile
    {
        public BabyFamilyAssetMapper()
        {
            CreateMap<BabyFamilyAsset, BabyFamilyAssetListDto>();
            CreateMap<BabyFamilyAssetListDto, BabyFamilyAsset>();

            CreateMap<BabyFamilyAssetEditDto, BabyFamilyAsset>();
            CreateMap<BabyFamilyAsset, BabyFamilyAssetEditDto>();
            CreateMap<BabyFamilyAsset, GetBabyFamilyAssetByPageOutputBasicInfo>().ForMember(s => s.ExpiredDateTitle, s => s.Ignore());

        }
    }
}
