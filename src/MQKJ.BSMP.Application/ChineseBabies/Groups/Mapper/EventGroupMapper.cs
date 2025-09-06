
using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.ChineseBabies.Mapper
{

	/// <summary>
    /// 配置EventGroup的AutoMapper
    /// </summary>
	internal static class EventGroupMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //configuration.CreateMap <EventGroup,EventGroupListDto>();
            //configuration.CreateMap <EventGroupListDto,EventGroup>();

            //configuration.CreateMap <EventGroupEditDto,EventGroup>();
            //configuration.CreateMap <EventGroup,EventGroupEditDto>();
         
            //configuration.CreateMap<GrowUpEvent, EventGroup>().ForMember(s => s, o => o.MapFrom(x => x.Event));
        }
	}
}
