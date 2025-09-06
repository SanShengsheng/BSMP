
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置EventGroupBabyEvent的AutoMapper
    /// </summary>
	internal static class EventGroupBabyEventMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <EventGroupBabyEvent,EventGroupBabyEventListDto>();
            configuration.CreateMap <EventGroupBabyEventListDto,EventGroupBabyEvent>();

            configuration.CreateMap <EventGroupBabyEventEditDto,EventGroupBabyEvent>();
            configuration.CreateMap <EventGroupBabyEvent,EventGroupBabyEventEditDto>();

        }
	}
}
