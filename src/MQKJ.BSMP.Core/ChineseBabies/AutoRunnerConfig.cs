using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.EnumHelper;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MQKJ.BSMP.ChineseBabies
{
    public class AutoRunnerConfig : ChineseBabyEntityBase
    {
        public int FamilyId { get; set; }
        public Guid PlayerId { get; set; }
        public int GroupId { get; set; }
        public int? ProfressionId { get; set; }
        public FamilyLevel FamilyLevel { get; set; }
        /// <summary>
        /// 成长事件消费
        /// </summary>
        public ConsumeLevel ConsumeLevel { get; set; }
        /// <summary>
        /// 学习事件消耗
        /// </summary>
        public ConsumeLevel StudyLevel { get; set; }
        /// <summary>
        /// 精力购买次数
        /// </summary>
        public int BuyCount { get; set; }
        public int StudyCount { get; set; }
        public BabyProperty? BabyProperty { get; set; }
        public AutoRunnerState State { get; set; }
        public EventGroup Group { get; set; }
        public Player Player { get; set; }
        public Family Family { get; set; }
        public Profession Profession { get; set; }
        public LevelAction LevelAction { get; set; }
    }

    /// <summary>
    /// 家庭档位设置
    /// </summary>
    public enum LevelAction
    {
        [EnumDescription("保持档位")]
        Keep = 0,
        [EnumDescription("不")]
        NotKeep = 1
    }


    public enum AutoRunnerState
    {
        NotRun = 1,
        Running = 2,
        Cancel = 3,
        Finished = 4
    }


    public enum ConsumeLevel
    {
        [EnumDescription("经济型")]
        Low = 1,
        [EnumDescription("中等")]
        Middle = 2,
        [EnumDescription("豪华型")]
        Hight = 3,
        [EnumDescription("随机")]
        Random = 4
    }
}
