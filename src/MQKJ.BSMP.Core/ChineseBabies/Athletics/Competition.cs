using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athletics
{
    /// <summary>
    /// 参赛表
    /// </summary>
    [Table("Competitions")]
    public class Competition:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 家庭Id
        /// </summary>
        public virtual int FamilyId { get; set; }

        public virtual Family Family { get; set; }


        /// <summary>
        /// 宝宝Id
        /// </summary>
        public int BabyId { get; set; }


        public Baby Baby { get; set; }

        ///// <summary>
        ///// 带宝宝的玩家Id
        ///// </summary>
        //public Guid? PlayerId { get; set; }

        /// <summary>
        /// 赛季Id
        /// </summary>
        public int SeasonManagementId { get; set; }

        public SeasonManagement SeasonManagement { get; set; }


        /// <summary>
        /// 爸爸剩余次数
        /// </summary>
        public int FatherFightCount { get; set; }


        /// <summary>
        /// 妈妈剩余次数
        /// </summary>
        public int MotherFightCount { get; set; }

        /// <summary>
        /// 输的次数
        /// </summary>
        public int WiningCount { get; set; }

        /// <summary>
        /// 赢的次数
        /// </summary>
        public int FailedCount { get; set; }

        /// <summary>
        /// 排名
        /// </summary>
        //public int RankingNumber { get; set; }


        /// <summary>
        /// 总积分
        /// </summary>
        public int GamePoint { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 段位
        /// </summary>
        public DanGrading DanGrading { get; set; }
    }

    /// <summary>
    /// 段位
    /// </summary>
    public enum DanGrading
    {
        UnKnow = 0,

        /// <summary>
        /// 青铜
        /// </summary>
        [EnumHelper.EnumDescription("青铜")]
        Bronze = 1,


        /// <summary>
        /// 白银
        /// </summary>
        [EnumHelper.EnumDescription("白银")]
        Silver = 2,


        /// <summary>
        /// 黄金
        /// </summary>
        [EnumHelper.EnumDescription("黄金")]
        Gold = 3,


        /// <summary>
        /// 钻石
        /// </summary>
        [EnumHelper.EnumDescription("钻石")]
        Diamond = 4,


        /// <summary>
        /// 王者
        /// </summary>
        [EnumHelper.EnumDescription("王者")]
        King = 5,

    }
}
