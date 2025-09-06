using Abp.AutoMapper;

namespace MQKJ.BSMP.ChineseBabies.BabySystem.Dtos.HostDtos
{
    /// <summary>
    /// 学习事件excel导入
    /// </summary>
    [AutoMapTo(typeof(BabyEvent))]
    public class ImportEventDto
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public int groupid { get; set; }
        public string own { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountDown { get; set; }
        public string depict_1 { get; set; }
        public int deplete_1 { get; set; }
        public int reward_1 { get; set; }
        public string depict_2 { get; set; }
        public int deplete_2 { get; set; }
        public int reward_2 { get; set; }
        public string depict_3 { get; set; }
        public int deplete_3 { get; set; }
        public int reward_3 { get; set; }
        public string imagePath { get; set; }
        /// <summary>
        /// 是否为道具，用于判断是否为家庭资产，以下2,3均表示此意
        /// </summary>
        public int means_1 { get; set; }

        public int means_2 { get; set; }

        public int means_3 { get; set; }


        /// <summary>
        /// 事件编码
        /// </summary>
        public int EventCode
        {
            get; set;
        }
        /// <summary>
        /// 操作类型
        /// </summary>
        public int OperationType { get; set; }
        /// <summary>
        /// 触发条件类型
        /// </summary>
        public int ConditionType { get; set; }

        public int BabyProperty { get; set; }

        /// <summary>
        /// 触发事件的最大值(属性值)
        /// </summary>
        public int MaxValue { get; set; }


        /// <summary>
        /// 触发事件的最小值(属性值)
        /// </summary>
        public int MinValue { get; set; }

        /// <summary>
        /// 事件触发的活动
        /// </summary>
        public int ActivityId { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public double Age { get; set; }
        public string AgeString { get; set; }
        /// <summary>
        /// 学习类型
        /// </summary>
        public int StudyType { get; set; }
        /// <summary>
        /// 允许学习的最大次数
        /// </summary>
        public int StudyAllowMaxTime { get; set; }
        /// <summary>
        /// 过期事件，过期组编号，仅对特殊事件，该字段表明
        /// </summary>
        public int ExpirationGroupId { get; set; }
        /// <summary>
        /// 工资
        /// </summary>
        public int Wage { get; set; }

        public int PreEventCode { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 选项 1 编号
        /// </summary>
        public int Option_1_code { get; set; }
        /// <summary>
        /// 选项 2 编号
        /// </summary>
        public int Option_2_code { get; set; }
        /// <summary>
        /// 选项 3 编号
        /// </summary>
        public int Option_3_code { get; set; }
        /// <summary>
        /// 前置事件组Id
        /// </summary>
        public int PrevGroupId { get; set; }
        /// <summary>
        /// 旁白
        /// </summary>
        public string Aside { get; set; }

    }
}
