
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置EnergyRechargeRecord的AutoMapper
    /// </summary>
	internal static class EnergyRechargeRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <EnergyRechargeRecord,EnergyRechargeRecordListDto>();
            configuration.CreateMap <EnergyRechargeRecordListDto,EnergyRechargeRecord>();

            configuration.CreateMap <EnergyRechargeRecordEditDto,EnergyRechargeRecord>();
            configuration.CreateMap <EnergyRechargeRecord,EnergyRechargeRecordEditDto>();

        }
	}
}
