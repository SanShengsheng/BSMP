using AutoMapper;
using MQKJ.BSMP.GameRecords;

namespace MQKJ.BSMP.GameRecords.Dtos
{

	/// <summary>
	/// 配置GameRecord的AutoMapper
	/// </summary>
	internal static class CustomerGameRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <GameRecord, GameRecordListDto>();
            configuration.CreateMap <GameRecordEditDto, GameRecord>();
		
		    
			
		    //// custom codes 
		    
            //// custom codes end

        }
    }
}