using AutoMapper;
using MQKJ.BSMP.BonusPointRecords;
using MQKJ.BSMP.BonusPoints;

namespace MQKJ.BSMP.BonusPointRecords.Dtos
{

	/// <summary>
	/// 配置BonusPointRecord的AutoMapper
	/// </summary>
	internal static class CustomerBonusPointRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <BonusPointRecord, BonusPointRecordListDto>();
            configuration.CreateMap <BonusPointRecordEditDto, BonusPointRecord>();
		
		    
			
		    //// custom codes 
		    
            //// custom codes end

        }
    }
}