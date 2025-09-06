
using AutoMapper;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.Orders.Dtos;

namespace MQKJ.BSMP.Orders.Mapper
{

	/// <summary>
    /// 配置Order的AutoMapper
    /// </summary>
	internal static class OrderMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Order,OrderListDto>().ForMember(o => o.TenantName,option => option.Ignore())
                .ForMember(o => o.AgentName, option => option.Ignore());

            configuration.CreateMap <OrderListDto,Order>();

            configuration.CreateMap <OrderEditDto,Order>();
            configuration.CreateMap <Order,OrderEditDto>();

        }
	}
}
