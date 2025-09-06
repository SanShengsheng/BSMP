
using AutoMapper;
using MQKJ.BSMP.ApplicationLogs;
using MQKJ.BSMP.ApplicationLogs.Dtos;

namespace MQKJ.BSMP.ApplicationLogs.Mapper
{

	/// <summary>
    /// 配置ApplicationLog的AutoMapper
    /// </summary>
	internal static class ApplicationLogMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <ApplicationLog,ApplicationLogListDto>();
            configuration.CreateMap <ApplicationLogListDto,ApplicationLog>();

            configuration.CreateMap <ApplicationLogEditDto,ApplicationLog>();
            configuration.CreateMap <ApplicationLog,ApplicationLogEditDto>();

        }
	}
}
