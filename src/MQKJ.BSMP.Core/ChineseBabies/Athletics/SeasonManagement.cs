using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athletics
{
    /// <summary>
    /// 赛季管理表
    /// </summary>
    [Table("SeasonManagements")]
    public class SeasonManagement : FullAuditedEntity
    {
        /// <summary>
        /// 赛季编号
        /// </summary>
        public int SeasonNumber { get; set; }

        /// <summary>
        /// 赛季开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 赛季准备时间
        /// </summary>
        //public DateTime ReadyTime { get; set; }

        /// <summary>
        /// 赛季结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 是否是当前的版本号
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// 本赛季排行榜显示的玩家数
        /// </summary>
        public int RankingShowPlayerCount { get; set; }


        /// <summary>
        /// 本赛季可PK
        /// </summary>
        public int CanPKCount { get; set; }

        /// <summary>
        /// 奖励玩家个数
        /// </summary>
        public int RewardPlayerCount { get; set; }


        /// <summary>
        /// 每天默认送的对战次数（妈妈）
        /// </summary>
        public int MotherDefaultFightCount { get; set; }

        /// <summary>
        /// 每天默认送的对战次数(爸爸)
        /// </summary>
        public int FatherDefaultFightCount { get; set; }

        /// <summary>
        /// 本期对战次数最大限制
        /// </summary>
        public int MaxFightCount { get; set; }

        /// <summary>
        /// 对战次数单价
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// 版本描述
        /// </summary>
        public string Description { get; set; }
    }
}
