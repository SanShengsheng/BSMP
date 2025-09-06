using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.StaminaRecords
{
    [Table("StaminaRecords")]
    public class StaminaRecord : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 扣掉或增加的体力值
        /// </summary>
        public int StaminaCount { get; set; }

        /// <summary>
        /// 玩家Id
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 玩家实体
        /// </summary>
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

    }
}
