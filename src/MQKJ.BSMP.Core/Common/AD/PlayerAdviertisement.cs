using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.Common.AD
{
    [Table("PlayerAdviertisements")]
    public class PlayerAdviertisement:Entity<Guid>,ISoftDelete,IHasCreationTime,IHasModificationTime
    {
        public Guid PlayerId { get; set; }

        public virtual Player Player { get; set; }

        public int AdviertisementId { get; set; }
        public virtual Adviertisement Adviertisement { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// 设备UUID
        /// </summary>
        public string UUID { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }
    }
}
