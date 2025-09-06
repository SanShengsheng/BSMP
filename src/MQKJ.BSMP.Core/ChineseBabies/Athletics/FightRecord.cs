using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athletics
{
    /// <summary>
    /// 对战记录表
    /// </summary>
    [Table("FightRecords")]
    public class FightRecord : FullAuditedEntity<Guid>
    {
        ///// <summary>
        ///// 家庭Id
        ///// </summary>
        //public int? FamilyId { get; set; }


        /// <summary>
        /// 发起人Id（宝宝父母）
        /// </summary>
        public Guid InitiatorId { get; set; }

        /// <summary>
        /// 发起人
        /// </summary>
        public Player Initiator { get; set; }

        /// <summary>
        /// 发起人宝宝Id
        /// </summary>
        public int InitiatorBabyId { get; set; }

        //[InverseProperty("InitiatorBabyId")]
        public Baby InitiatorBaby { get; set; }


        /// <summary>
        /// 对方宝宝Id
        /// </summary>
        public int OtherBabyId { get; set; }

        //[InverseProperty("OtherBabyId")]
        public Baby OtherBaby { get; set; }

        /// <summary>
        /// 胜率
        /// </summary>
        public double WinningRatio { get; set; }

        /// <summary>
        /// 道具加成(胜率)
        /// </summary>
        public double PropAdditionRate { get; set; }

        /// <summary>
        /// 属性加成
        /// </summary>
        public double AttributeRate { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public double RandomNumber { get; set; }

        ///// <summary>
        ///// 赢的宝宝Id
        ///// </summary>
        //public int? WinnerId { get; set; }

        ///// <summary>
        ///// 输的宝宝Id
        ///// </summary>
        //public int? failederId { get; set; }

        /// <summary>
        /// 宝宝属性编码
        /// </summary>
        public BabyAttributeCode BabyAttributeCode { get; set; }

        /// <summary>
        /// 赛季管理Id
        /// </summary>
        public int SeasonManagementId { get; set; }

        public SeasonManagement SeasonManagement { get; set; }



        /// <summary>
        /// 积分增减量
        /// </summary>
        public int GamePoint { get; set; }

        /// <summary>
        /// 上个赛季积分
        /// </summary>
        public int LastTimePoint { get; set; }


        /// <summary>
        /// 当前赛季积分
        /// </summary>
        public int CurrentPoint { get; set; }

        /// <summary>
        /// 上次段位
        /// </summary>
        public DanGrading LastDangrading { get; set; }


        /// <summary>
        /// 上个赛季排名
        /// </summary>
        //public int LastTimeRanking { get; set; }


        /// <summary>
        /// 当前赛季排名
        /// </summary>
        //public int CurrentRanking { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 对战结果
        /// </summary>
        public FightResultEnum FightResultEnum { get; set; }
    }

    public enum FightResultEnum
    {
        Intialize = 0,

        /// <summary>
        /// 赢
        /// </summary>
        Win = 1,

        /// <summary>
        /// 输
        /// </summary>
        Fail = 2
    }

    /// <summary>
    /// 宝宝属性编码
    /// </summary>
    public enum BabyAttributeCode
    {
        UnKnow = 0,

        /// <summary>
        /// 智力编码
        /// </summary>
        IntelligenceCode = 1000,

        /// <summary>
        /// 体魄编码
        /// </summary>
        PhysiqueCode = 1001,

        /// <summary>
        /// 想象编码
        /// </summary>
        ImagineCode = 1002,

        /// <summary>
        /// 意志编码
        /// </summary>
        WillPowerCode = 1003,

        /// <summary>
        /// 情商编码
        /// </summary>
        EmotionQuotientCode = 1004,

        /// <summary>
        /// 魄力编码
        /// </summary>
        CharmCode = 1005,

        /// <summary>
        /// 健康编码
        /// </summary>
        HealthyCode = 1006,

        /// <summary>
        /// 精力编码
        /// </summary>
        EnergyCode = 1007
    }
}
