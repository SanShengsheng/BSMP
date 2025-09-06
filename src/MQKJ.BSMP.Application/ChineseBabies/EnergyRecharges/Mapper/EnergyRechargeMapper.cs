
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置EnergyRecharge的AutoMapper
    /// </summary>
	internal static class EnergyRechargeMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <EnergyRecharge,EnergyRechargeListDto>();
            configuration.CreateMap <EnergyRechargeListDto,EnergyRecharge>();

            configuration.CreateMap <EnergyRechargeEditDto,EnergyRecharge>();
            configuration.CreateMap <EnergyRecharge,EnergyRechargeEditDto>();

        }
	}
}
