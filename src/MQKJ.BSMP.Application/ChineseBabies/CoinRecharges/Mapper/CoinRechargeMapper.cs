
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置CoinRecharge的AutoMapper
    /// </summary>
	internal static class CoinRechargeMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <CoinRecharge,CoinRechargeListDto>().ForMember(p => p.IsFirstCharge, option => option.Ignore());
            configuration.CreateMap <CoinRechargeListDto,CoinRecharge>();

            configuration.CreateMap <CoinRechargeEditDto,CoinRecharge>();
            configuration.CreateMap <CoinRecharge,CoinRechargeEditDto>();
        }
	}
}
