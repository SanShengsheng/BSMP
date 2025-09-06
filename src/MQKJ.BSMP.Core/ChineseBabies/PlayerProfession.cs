using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Players;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.ChineseBabies
{
    [Table("PlayerProfessions")]
    public class PlayerProfession : FullAuditedEntity<int>
    {
        public Guid PlayerId { get; set; }
        public int FamilyId { get; set; }
        public int ProfessionId { get; set; }
        public Profession Profession { get; set; }
        public Family Family { get; set; }
        public Player Player { get; set; }
        public bool IsCurrent { get; set; }

        /// <summary>
        /// 是否虚拟转职
        /// </summary>
        public bool IsVirtualRecharge { get; set; }
   
    }
}