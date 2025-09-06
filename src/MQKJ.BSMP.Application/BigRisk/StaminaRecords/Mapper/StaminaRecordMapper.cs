
using AutoMapper;
using MQKJ.BSMP.StaminaRecords;
using MQKJ.BSMP.StaminaRecords.Dtos;

namespace MQKJ.BSMP.StaminaRecords.Mapper
{

	/// <summary>
    /// 配置StaminaRecord的AutoMapper
    /// </summary>
	internal static class StaminaRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <StaminaRecord,StaminaRecordListDto>();
            configuration.CreateMap <StaminaRecordListDto,StaminaRecord>();

            configuration.CreateMap <StaminaRecordEditDto,StaminaRecord>();
            configuration.CreateMap <StaminaRecord,StaminaRecordEditDto>();

        }
	}
}
