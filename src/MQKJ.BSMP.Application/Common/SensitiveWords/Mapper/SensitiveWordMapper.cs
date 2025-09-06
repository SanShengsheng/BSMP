
using AutoMapper;
using MQKJ.BSMP.Common.SensitiveWords;
using MQKJ.BSMP.Common.SensitiveWords.Dtos;

namespace MQKJ.BSMP.Common.SensitiveWords.Mapper
{

	/// <summary>
    /// 配置SensitiveWord的AutoMapper
    /// </summary>
	internal static class SensitiveWordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <SensitiveWord,SensitiveWordListDto>();
            configuration.CreateMap <SensitiveWordListDto,SensitiveWord>();

            configuration.CreateMap <SensitiveWordEditDto,SensitiveWord>();
            configuration.CreateMap <SensitiveWord,SensitiveWordEditDto>();

        }
	}
}
