using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.BonusPoints
{
    [Table("BonusPointRecords")]
    public class BonusPointRecord : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 获取爱豆数量 + -
        /// </summary>
        public virtual int GatherCount { get; set; }

        /// <summary>
        /// 玩家Id
        /// </summary>
        public virtual Guid PlayerId { get; set; }

        /// <summary>
        /// 玩家实体
        /// </summary>
        //[ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }

        public virtual int BonusPointId { get; set; }

        [ForeignKey("BonusPointId")]
        public virtual BonusPoint BonusPoint { get; set; }
    }
}
