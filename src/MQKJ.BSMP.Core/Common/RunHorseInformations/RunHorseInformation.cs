using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.Common.RunHorseInformations
{
    /// <summary>
    /// 跑马灯消息表
    /// </summary>
    [Table("RunHorseInformations")]
    public class RunHorseInformation:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 产生者
        /// </summary>
        //public Guid? PlayerId { get; set; }

        //public Guid ReceiverId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 开始时间 null表示无限播放
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间 null表示无限播放
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 播放间隔  默认3秒
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// 播放次数  -1表示无限播放 默认-1
        /// </summary>
        public int PlayCount { get; set; }

        /// <summary>
        /// 播放场景
        /// </summary>
        public PlayScene PlayScene { get; set; }

        /// <summary>
        /// 优先级 1-最高
        /// </summary>
        public int Priority { get; set; }
    }

    public enum PlayScene
    {
        /// <summary>
        /// 宝宝页
        /// </summary>
        BabyPage = 1,

        /// <summary>
        /// 竞技场
        /// </summary>
        AthleticsPage = 2,
    }

    public enum RunHorseInformationSource
    {
        /// <summary>
        /// 充值
        /// </summary>
        Recharge = 1,

        /// <summary>
        /// 宝宝出生
        /// </summary>
        BabyBorn = 2,

        /// <summary>
        /// 运营
        /// </summary>
        Operate = 3,

        /// <summary>
        /// 竞技场对战
        /// </summary>
        AthleticsFight = 4,

        /// <summary>
        /// 竞技场排名消息
        /// </summary>
        AthleticsRanking = 5
    }
}
