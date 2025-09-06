
using AutoMapper;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Common.Dtos;

namespace MQKJ.BSMP.Common.Mapper
{

	/// <summary>
    /// 配置WeChatWebUser的AutoMapper
    /// </summary>
	internal static class WeChatWebUserMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <WeChatWebUser,WeChatWebUserListDto>();
            configuration.CreateMap <WeChatWebUserListDto,WeChatWebUser>();

            configuration.CreateMap <WeChatWebUserEditDto,WeChatWebUser>();
            configuration.CreateMap <WeChatWebUser,WeChatWebUserEditDto>();

        }
	}
}
