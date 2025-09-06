
using AutoMapper;
using MQKJ.BSMP.Friends;
using MQKJ.BSMP.Friends.Dtos;

namespace MQKJ.BSMP.Friends.Mapper
{

	/// <summary>
    /// 配置Friend的AutoMapper
    /// </summary>
	internal static class FriendMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Friend,FriendListDto>();
            configuration.CreateMap <FriendListDto,Friend>();

            configuration.CreateMap <FriendEditDto,Friend>();
            configuration.CreateMap <Friend,FriendEditDto>();

        }
	}
}
