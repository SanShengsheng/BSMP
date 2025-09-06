
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置CoinRechargeRecord的AutoMapper
    /// </summary>
	internal static class CoinRechargeRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <CoinRechargeRecord,CoinRechargeRecordListDto>();
            configuration.CreateMap <CoinRechargeRecordListDto,CoinRechargeRecord>();

            configuration.CreateMap <CoinRechargeRecordEditDto,CoinRechargeRecord>();
            configuration.CreateMap <CoinRechargeRecord,CoinRechargeRecordEditDto>();

        }
	}
}
