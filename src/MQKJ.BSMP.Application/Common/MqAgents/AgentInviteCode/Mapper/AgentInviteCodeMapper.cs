
using AutoMapper;
using MQKJ.BSMP.Common.MqAgents;
using MQKJ.BSMP.Common.MqAgents.Dtos;

namespace MQKJ.BSMP.Common.MqAgents.Mapper
{

	/// <summary>
    /// 配置AgentInviteCode的AutoMapper
    /// </summary>
	internal static class AgentInviteCodeMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <AgentInviteCode,AgentInviteCodeListDto>();
            configuration.CreateMap <AgentInviteCodeListDto,AgentInviteCode>();

            configuration.CreateMap <AgentInviteCodeEditDto,AgentInviteCode>();
            configuration.CreateMap <AgentInviteCode,AgentInviteCodeEditDto>();

        }
	}
}
