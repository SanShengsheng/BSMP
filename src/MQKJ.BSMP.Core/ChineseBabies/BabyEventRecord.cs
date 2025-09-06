using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Players;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 事件记录表
    /// </summary>
    [Table("BabyEventRecords")]
    public class BabyEventRecord : FullAuditedEntity<Guid>
    {
        public int FamilyId { get; set; }
        public Guid PlayerId { get; set; }
        public int EventId { get; set; }
        /// <summary>
        /// 增加宝宝编号
        /// </summary>
        public int BabyId { get; set; }
        public BabyEvent Event { get; set; }
        /// <summary>
        /// 事件状态
        /// </summary>
        public EventRecordState State { get; set; }
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间，时间戳
        /// </summary>
        public long? EndTimeStamp { get; set; }
        public DateTime? EndTime { get; set; }
        public int? OptionId { get; set; }
        public BabyEventOption Option { get; set; }
        /// <summary>
        /// 爸爸选什么
        /// </summary>
        public int? FatherOptionId { get; set; }
        /// <summary>
        /// 妈妈选什么
        /// </summary>
        public int? MotherOptionId { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        public Player Player { get; set; }
        public Family Family { get; set; }
        /// <summary>
        /// 事件组编号
        /// </summary>
        public int GroupId { get; set; }
    }
    public enum EventRecordState
    {
        /// <summary>
        /// 未处理
        /// </summary>
        UnHandle = 1,

        /// <summary>
        /// 处理中
        /// </summary>
        Handling = 2,

        /// <summary>
        /// 已处理
        /// </summary>
        Handled = 3,

        /// <summary>
        /// 等待另一个人处理
        /// </summary>
        WaitOther = 4
    }
}
