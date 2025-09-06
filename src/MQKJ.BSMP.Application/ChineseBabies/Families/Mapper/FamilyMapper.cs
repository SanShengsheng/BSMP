
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.Families.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置Family的AutoMapper
    /// </summary>
	internal static class FamilyMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Family,FamilyListDto>();
            configuration.CreateMap <FamilyListDto,Family>();

            configuration.CreateMap <FamilyEditDto,Family>();
            configuration.CreateMap <Family,FamilyEditDto>();

            configuration.CreateMap<Family, GetFamiliesWithPlayerIdOutput>().ForMember(x => x.Other,option => option.MapFrom(x => x.Father));

            configuration.CreateMap<Family, GetFamiliesWithPlayerIdOutput>().ForMember(x => x.Other, option => option.MapFrom(x => x.Mother));

        }
	}
}
