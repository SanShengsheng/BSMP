using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.Emoticons
{
    [Table("EmoticonRecords")]
    public class EmoticonRecord: FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 表情编号
        /// </summary>
        public virtual int Code { get; set; }

        /// <summary>
        /// 玩家Id
        /// </summary>
        public virtual Guid PlayerId { get; set; }

        /// <summary>
        /// 玩家实体
        /// </summary>
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }

        public virtual Guid GameTaskId { get; set; }

        [ForeignKey("GameTaskId")]
        public virtual GameTask GameTask { get; set; }

    }
}
