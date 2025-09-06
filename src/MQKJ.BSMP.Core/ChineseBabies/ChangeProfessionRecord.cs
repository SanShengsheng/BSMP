using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    public class ChangeProfessionRecord:FullAuditedEntity<Guid>
    {
        public int FamilyId { get; set; }

        public int ProfessionId { get; set; }

        public Guid PlayerId { get; set; }

        public Family Family { get; set; }

        public Profession Profession { get; set; }

        public decimal Cost { get; set; }
    }
}
