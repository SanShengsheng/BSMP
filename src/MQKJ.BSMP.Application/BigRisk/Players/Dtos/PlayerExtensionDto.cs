using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Players.Dtos
{
    [AutoMapTo(typeof(PlayerExtension))]
    public class PlayerExtensionDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 玩家编号
        /// </summary>
        public Guid PlayerGuid { get; set; }

        /// <summary>
        /// 游戏积分
        /// </summary>
        public int LoveScore { get; set; }


    }
}
