
using AutoMapper;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Common.Dtos;

namespace MQKJ.BSMP.Common.Mapper
{

	/// <summary>
    /// 配置EnterpirsePaymentRecord的AutoMapper
    /// </summary>
	internal static class EnterpirsePaymentRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <EnterpirsePaymentRecord,EnterpirsePaymentRecordListDto>();
            configuration.CreateMap <EnterpirsePaymentRecordListDto,EnterpirsePaymentRecord>();

            configuration.CreateMap <EnterpirsePaymentRecordEditDto,EnterpirsePaymentRecord>();
            configuration.CreateMap <EnterpirsePaymentRecord,EnterpirsePaymentRecordEditDto>();

        }
	}
}
