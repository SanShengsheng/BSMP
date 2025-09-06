using AutoMapper;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Players.WeChat.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos.CustomMapper
{
    /// <summary>
	/// 配置WeChat的AutoMapper
	/// </summary>
	internal static class CustomerWeChatMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //自定义映射
            var map = configuration.CreateMap<Player, GetPlayerInfoOutput>();
            map.ForMember(dto => dto.PlayerId, option => option.MapFrom(x => x.Id));
            map.ForMember(dto => dto.PlayerState, option => option.MapFrom(x => x.State));
            map.ForMember(dto => dto.Message, option => option.Ignore());
            map.ForMember(dto => dto.AvatarUrl, option => option.MapFrom(x => x.HeadUrl));
            map.ForMember(dto => dto.BonusPointsCount, option => option.Ignore());
            //map.ForMember(dto => dto.GameId, options => options.Ignore());



            var map1 = configuration.CreateMap<Player, GetPlayerInfoInput>();
            map1.ForMember(dto => dto.Code, option => option.Ignore());
            //map1.ForMember(dto => dto.WechatId, option => option.Ignore());

            //// custom codes 

            //// custom codes end

        }
    }
}
