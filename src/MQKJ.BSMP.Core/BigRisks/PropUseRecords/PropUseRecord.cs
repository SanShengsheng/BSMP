using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.PropUseRecords
{
    [Table("PropUseRecords")]
    public class PropUseRecord : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 道具类型
        /// </summary>
        public PropType PropType { get; set; }

        /// <summary>
        /// 玩家Id
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 玩家
        /// </summary>
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
    }
}
