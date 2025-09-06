
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置AutoRunnerRecord的AutoMapper
    /// </summary>
	internal static class AutoRunnerRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <AutoRunnerRecord,AutoRunnerRecordListDto>();
            configuration.CreateMap <AutoRunnerRecordListDto,AutoRunnerRecord>();

            configuration.CreateMap <AutoRunnerRecordEditDto,AutoRunnerRecord>();
            configuration.CreateMap <AutoRunnerRecord,AutoRunnerRecordEditDto>();

        }
	}
}
