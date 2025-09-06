
using AutoMapper;
using MQKJ.BSMP.Common.OperationActivities;
using MQKJ.BSMP.Common.OperationActivities.Dtos;

namespace MQKJ.BSMP.Common.OperationActivities.Mapper
{

	/// <summary>
    /// 配置OperationActivity的AutoMapper
    /// </summary>
	internal static class OperationActivityMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <OperationActivity,OperationActivityListDto>();
            configuration.CreateMap <OperationActivityListDto,OperationActivity>();

            configuration.CreateMap <OperationActivityEditDto,OperationActivity>();
            configuration.CreateMap <OperationActivity,OperationActivityEditDto>();

        }
	}
}
