using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    [Table("SupplementCoinRecords")]
    public class SupplementCoinRecord: FullAuditedEntity<Guid>
    {
        public Guid? OrderId { get; set; }

        public int FamilyId { get; set; }

        public int CoinCount { get; set; }

        public string Description { get; set; }
    }
}
