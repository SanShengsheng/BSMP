
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置BabyPropType的AutoMapper
    /// </summary>
	internal static class BabyPropTypeMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <BabyPropType,BabyPropTypeListDto>();
            configuration.CreateMap <BabyPropTypeListDto,BabyPropType>();

            configuration.CreateMap <BabyPropTypeEditDto,BabyPropType>();
            configuration.CreateMap <BabyPropType,BabyPropTypeEditDto>();

        }
	}
}
