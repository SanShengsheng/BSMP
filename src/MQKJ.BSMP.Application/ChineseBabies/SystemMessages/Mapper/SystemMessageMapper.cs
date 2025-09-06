
using AutoMapper;
using MQKJ.BSMP.SystemMessages;
using MQKJ.BSMP.SystemMessages.Dtos;

namespace MQKJ.BSMP.SystemMessages.Mapper
{

	/// <summary>
    /// 配置SystemMessage的AutoMapper
    /// </summary>
	internal static class SystemMessageMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <SystemMessage,SystemMessageListDto>();
            configuration.CreateMap <SystemMessageListDto,SystemMessage>();

            configuration.CreateMap <SystemMessageEditDto,SystemMessage>();
            configuration.CreateMap <SystemMessage,SystemMessageEditDto>();

        }
	}
}
