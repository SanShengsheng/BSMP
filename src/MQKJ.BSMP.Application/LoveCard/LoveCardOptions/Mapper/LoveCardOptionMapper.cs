
using AutoMapper;
using MQKJ.BSMP.LoveCardOptions;
using MQKJ.BSMP.LoveCardOptions.Dtos;

namespace MQKJ.BSMP.LoveCardOptions.Mapper
{

	/// <summary>
    /// 配置LoveCardOption的AutoMapper
    /// </summary>
	internal static class LoveCardOptionMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LoveCardOption,LoveCardOptionListDto>();
            configuration.CreateMap <LoveCardOptionListDto,LoveCardOption>();

            configuration.CreateMap <LoveCardOptionEditDto,LoveCardOption>();
            configuration.CreateMap <LoveCardOption,LoveCardOptionEditDto>();

        }
	}
}
