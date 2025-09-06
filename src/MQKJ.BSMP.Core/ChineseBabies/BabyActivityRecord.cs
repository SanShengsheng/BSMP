using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 宝宝活动记录表
    /// </summary>
    [Table("BabyActivityRecords")]
    public class BabyActivityRecord:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 发起活动人的Id
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 接收活动人的Id
        /// </summary>
        public Guid ReceiverId { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// 发起活动人家庭Id
        /// </summary>
        public int FamilyId { get; set; }
        public Family Family { get; set; }
        public BabyActivity Activity { get; set; }
        /// <summary>
        /// 是否已经领取金币
        /// </summary>
        public bool IsGet { get; set; }
    }
}
