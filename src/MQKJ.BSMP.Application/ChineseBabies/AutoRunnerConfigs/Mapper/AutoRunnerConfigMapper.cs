
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置AutoRunnerConfig的AutoMapper
    /// </summary>
	internal static class AutoRunnerConfigMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <AutoRunnerConfig,AutoRunnerConfigListDto>();
            configuration.CreateMap <AutoRunnerConfigListDto,AutoRunnerConfig>();

            //configuration.CreateMap <AutoRunnerConfigEditDto,AutoRunnerConfig>();
            //configuration.CreateMap <AutoRunnerConfig,AutoRunnerConfigEditDto>();

        }
	}
}
