using AutoMapper;
using MQKJ.BSMP.PropUseRecords;
namespace MQKJ.BSMP.PropUseRecords.Dtos
{

	/// <summary>
	/// 配置PropUseRecord的AutoMapper
	/// </summary>
	internal static class CustomerPropUseRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <PropUseRecord, PropUseRecordListDto>();
            configuration.CreateMap <PropUseRecordEditDto, PropUseRecord>();
		
		    
			
		    //// custom codes 
		    
            //// custom codes end

        }
    }
}