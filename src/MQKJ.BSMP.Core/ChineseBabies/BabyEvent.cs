using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.EnumHelper;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 事件表
    /// </summary>
    [Table("BabyEvents")]
    public class BabyEvent : FullAuditedEntity
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public IncidentType Type { get; set; }


        /// <summary>
        /// 是否阻断
        /// </summary>
        public bool IsBlock { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public OperationType OperationType { get; set; }
        /// <summary>
        /// 旁白
        /// </summary>
        public string Aside { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 倒计时
        /// </summary>
        public int CountDown { get; set; }
        /// <summary>
        /// 触发条件类型
        /// </summary>
        public ConditionType ConditionType { get; set; }
        /// <summary>
        /// 触发事件编号
        /// </summary>
        public int? EventId { get; set; }
        public BabyProperty? BabyProperty { get; set; }

        /// <summary>
        /// 触发事件的最大值(属性值)
        /// </summary>
        public int? MaxValue { get; set; }


        /// <summary>
        /// 触发事件的最小值(属性值)
        /// </summary>
        public int? MinValue { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 事件的背景图
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// 事件触发的活动
        /// </summary>
        public int? ActivityId { get; set; }

        /// <summary>
        /// 事件奖励id
        /// </summary>
        public int? RewardId { get; set; }

        /// <summary>
        /// 事件消耗Id
        /// </summary>
        public int? ConsumeId { get; set; }
        public Reward Reward { get; set; }
        public Reward Consume { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public double? Age { get; set; }

        public  string AgeString { get; set; }

        /// <summary>
        /// 学习类型
        /// </summary>
        public StudyType? StudyType { get; set; }
        /// <summary>
        /// 允许学习的最大次数
        /// </summary>
        public int? StudyAllowMaxTime { get; set; }
        /// <summary>
        /// 过期事件编号，仅对特殊事件，该字段表明
        /// </summary>
        public int? ExpirationGroupId { get; set; }
    
        /// <summary>
        /// 工资
        /// </summary>
        public int? Wage { get; set; }

        /// <summary>
        /// 选项编码，导入数据使用
        /// </summary>
        public int? Code { get; set; }

        public int? PreEventId { get; set; }

        public virtual IEnumerable<BabyEventOption> Options { get; set; }
        public virtual IEnumerable<BabyEventRecord> BabyEventRecords { get; set; }
        public virtual IEnumerable<EventGroupBabyEvent> EventGroupBabyEvents { get; set; }
    }
    public enum BabyProperty
    {
        /// <summary>
        /// 智力
        /// </summary>
        Intelligence = 0,
        /// <summary>
        /// 体魄
        /// </summary>
        Physique = 1,
        /// <summary>
        /// 想象力
        /// </summary>
        Imagine = 2,
        /// <summary>
        /// 意志
        /// </summary>
        WillPower = 3,
        /// <summary>
        /// 情商
        /// </summary>
        EmotionQuotient = 4,
        /// <summary>
        /// 魅力
        /// </summary>
        Charm = 5,
        /// <summary>
        /// 健康
        /// </summary>
        Healthy = 6,
        /// <summary>
        /// 精力
        /// </summary>
        Energy = 7,

        /// <summary>
        /// 幸福度
        /// </summary>
        Happiness = 99,
    }
    public enum ConditionType
    {
        /// <summary>
        /// 任务ID触发
        /// </summary>
        [Description("任务ID触发")]
        Event = 1,
        /// <summary>
        /// 属性触发
        /// </summary>
        [Description("属性触发")]
        Property = 2,
        /// <summary>
        /// 不触发
        /// </summary>
        [Description("不触发")]
        Normal = 0
    }
    public enum OperationType
    {
        /// <summary>
        /// 单人
        /// </summary>
        Single = 1,

        /// <summary>
        /// 双人
        /// </summary>
        Double = 2
    }

    public enum IncidentType
    {
        [Description("成长事件")]
        Growup = 1,
        [Description("学习事件")]
        Study = 2,
        [Description("特殊事件")]
        Special = 3,
        [Description("阻断事件")]
        Block = 4
    }

    public enum StudyType
    {
        /// <summary>
        /// 协作类
        /// </summary>
        [EnumDescription("协作类")]
        Team = 0,
        /// <summary>
        /// 交际类
        /// </summary>
        [EnumDescription("交际类")]
        Communication = 1,
        /// <summary>
        /// 性格类
        /// </summary>
        [EnumDescription("性格类")]
        Character = 2,
        /// <summary>
        /// 运动类
        /// </summary>
        [EnumDescription("运动类")]
        Sport = 3,
        /// <summary>
        /// 开发
        /// </summary>
        [EnumDescription("开发类")]
        Exploit = 4,
        /// <summary>
        /// 创造类
        /// </summary>
        [EnumDescription("创造类")]
        Creativity=5
    }
}
