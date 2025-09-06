using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Players.WeChat.Dtos
{
    public class GetPlayerInfoOutput
    {
        /// <summary>
        /// 玩家状态
        /// </summary>
        public PlayerState PlayerState { get; set; }

        /// <summary>
        /// 玩家Id
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 年龄层次
        /// </summary>
        public int AgeRange { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }

        public string NickName { get; set; }


        /// <summary>
        /// AvatarUrl
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 积分数量
        /// </summary>
        public int BonusPointsCount { get; set; }

        /// <summary>
        /// 体力值
        /// </summary>
        public int Stamina { get; set; }

        ///// <summary>
        ///// 积分增减量
        ///// </summary>
        //public int BonusPointsCount { get; set; }

        /// <summary>
        /// 生成房間ID返回给你小程序
        /// </summary>
        //public Guid GameId { get; set; }

        /// <summary>
        /// 信息修改次数
        /// </summary>
        public int ModifyCount { get; set; }
        public bool IsAddMask { get; set; }

        public bool IsHasIdentity { get; set; }
    }
}
