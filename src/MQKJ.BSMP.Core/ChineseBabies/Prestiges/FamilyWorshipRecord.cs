using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Prestiges
{
    /// <summary>
    /// 家庭膜拜记录表
    /// </summary>
    [Table("FamilyWorshipRecords")]
    public class FamilyWorshipRecord:Entity<int>, IHasCreationTime,ISoftDelete
    {
        /// <summary>
        /// 膜拜方家庭
        /// </summary>
        public int FromFamilyId { get; set; }
        /// <summary>
        /// 被膜拜方家庭
        /// </summary>
        public virtual int ToFamilyId { get; set; }
        [ForeignKey("ToBabyId")]
        public virtual Baby ToBaby { get; set; } 
        /// <summary>
        /// 被膜拜方宝宝
        /// </summary>
        public int ? ToBabyId { get; set; }
        /// <summary>
        /// 膜拜方宝宝
        /// </summary>
        public int ? FromBabyId { get; set; }
        [ForeignKey("FromBabyId")]
        public Baby FromBaby { get; set; }
        /// <summary>
        /// 获得声望值
        /// </summary>
        public int Prestiges { get; set; }
        /// <summary>
        /// 获得的金币
        /// </summary>
        public int Coins { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
