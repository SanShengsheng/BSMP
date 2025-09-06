using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.GameTasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.GameRecords
{
    [Table("GameRecords")]
    public class GameRecord : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 记录时间
        /// </summary>
        //public DateTime RecordTime { get; set; }

        /// <summary>
        /// 记录的状态
        /// </summary>
        public TaskState State { get; set; }

        /// <summary>
        /// GameTask外键Id
        /// </summary>
        public Guid GameTaskId { get; set; }

        /// <summary>
        /// 存放题编号 逗号隔开
        /// </summary>
        public string QuestionIds { get; set; }

        /// <summary>
        /// GameTask实体
        /// </summary>
        [ForeignKey("GameTaskId")]
        public GameTask GameTask { get; set; }

    }
}
