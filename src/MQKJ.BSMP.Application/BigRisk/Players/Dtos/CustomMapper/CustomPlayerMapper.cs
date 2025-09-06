using AutoMapper;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.Players.Dtos
{

	/// <summary>
	/// 配置Player的AutoMapper
	/// </summary>
	internal static class CustomerPlayerMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Player, PlayerListDto>();
            configuration.CreateMap <PlayerEditDto, Player>();

            //自定义映射
            //var map = configuration.CreateMap<Player, PlayerListDto>();
            //map.ForMember(dto => dto.OpenId, option => option.Ignore());
            //// custom codes 

            //// custom codes end

        }
    }
}