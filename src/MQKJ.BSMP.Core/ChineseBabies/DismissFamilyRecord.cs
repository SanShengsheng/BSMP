using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    [Table("DismissFamilyRecords")]
    public class DismissFamilyRecord: FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 家庭Id
        /// </summary>
        public int FamilyId { get; set; }

        public Family Family { get; set; }

        /// <summary>
        /// 解散类型
        /// </summary>
        public DismissFamilyType DismissFamilyType { get; set; }

        /// <summary>
        /// 发起者
        /// </summary>
        public Guid? InitiatorId { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public Guid? OrderId { get; set; }

        /// <summary>
        /// 家庭状态
        /// </summary>
        public FamilyState FamilyState { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
