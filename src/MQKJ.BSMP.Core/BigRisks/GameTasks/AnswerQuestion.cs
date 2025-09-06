using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Questions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.GameTasks
{
    [Table("AnswerQuestions")]
    public class AnswerQuestion : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 邀请方答案
        /// </summary>
        public int InviterAnswerId { get; set; }

        /// <summary>
        /// 被邀请方答案
        /// </summary>
        public int InviteeAnswerId { get; set; }

        ///// <summary>
        ///// 答题开始时间
        ///// </summary>
        //public DateTime StartTime { get; set; }

        ///// <summary>
        ///// 答题结束时间
        ///// </summary>
        //public DateTime EndTime { get; set; }

        /// <summary>
        /// 答题状态 错误或者正确
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 问题 Id
        /// </summary>
        public int QuesionId { get; set; }

        [ForeignKey("QuesionId")]
        public Question Question { get; set; }

        /// <summary>
        /// 任务Id
        /// </summary>
        public Guid GameTaskId { get; set; }

        /// <summary>
        /// 任务实体
        /// </summary>
        [ForeignKey("GameTaskId")]
        public GameTask GameTask { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int InviterAnswerSort { get; set; }

        public int InviteeAnswerSort { get; set; }

        public int Floor { get; set; }

        /// <summary>
        /// 是否作弊
        /// </summary>
        public bool  IsCheat { get; set; }

      

    }
}
