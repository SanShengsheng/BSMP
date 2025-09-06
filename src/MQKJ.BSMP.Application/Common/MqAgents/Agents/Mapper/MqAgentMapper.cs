
using AutoMapper;
using MQKJ.BSMP;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos;
using MQKJ.BSMP.Dtos;

namespace MQKJ.BSMP.Mapper
{

	/// <summary>
    /// 配置MqAgent的AutoMapper
    /// </summary>
	internal static class MqAgentMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <MqAgent,MqAgentListDto>();
            configuration.CreateMap <MqAgentListDto,MqAgent>();

            //configuration.CreateMap <MqAgentEditDto,MqAgent>();
            //configuration.CreateMap <MqAgent,MqAgentEditDto>();

            configuration.CreateMap<MqAgent, GetAgentInfoOutput>().ForMember(a => a.TotalIncome, option => option.Ignore());


            configuration.CreateMap<MqAgent, GetSecondAgentListDto>().ForMember(a => a.CreateTime, option => option.MapFrom(a => a.CreationTime));


            

        }
	}
}
