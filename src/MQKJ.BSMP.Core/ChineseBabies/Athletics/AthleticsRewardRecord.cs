using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athletics
{
    /// <summary>
    /// 竞技场奖励记录表
    /// </summary>
    [Table("AthleticsRewardRecords")]
    public class AthleticsRewardRecord:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 宝宝Id
        /// </summary>
        public int? BabyId { get; set; }

        /// <summary>
        /// 家庭Id
        /// </summary>
        public int? FamilyId { get; set; }

        public int AthleticsRewardId { get; set; }

        public AthleticsReward AthleticsReward { get; set; }

    }
}
