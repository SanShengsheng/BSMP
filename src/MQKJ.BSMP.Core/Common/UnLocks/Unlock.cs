using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.UnLocks
{
    [Table("Unlocks")]
    public class Unlock:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 解锁人的Id
        /// </summary>
        public Guid UnLockerId { get; set; }

        /// <summary>
        /// 被解锁人的Id
        /// </summary>
        public Guid BeUnLockerId { get; set; }
    }
}
