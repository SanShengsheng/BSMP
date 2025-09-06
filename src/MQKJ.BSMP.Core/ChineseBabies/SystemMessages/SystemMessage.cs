using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.SystemMessages
{
    [Table("SystemMessages")]
    public class SystemMessage : FullAuditedEntity
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 通知类别
        /// </summary>
        public NoticeType NoticeType { get; set; }
        /// <summary>
        /// 周期类别
        /// </summary>
        public PeriodType PeriodType { get; set; }
        /// <summary>
        /// 优先级，1~100,100为最高
        /// </summary>
        public int PriorityLevel { get; set; }
        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime ExprieDateTime { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public int? Code { get; set; }
        /// <summary>
        /// 次数，默认为1
        /// </summary>
        [DefaultValue(1)]
        public int Count { get; set; }
        /// <summary>
        /// 周期，间隔
        /// </summary>
        public int Period { get; set; }

     
    }

    public enum NoticeType
    {
        All = 1,//所有用户
    }

    public enum PeriodType
    {
        NeverStop = 1,//从不停止
        Minute = 2,
    }

}