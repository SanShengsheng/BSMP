using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    [Table("EventGroupBabyEvents")]
    public class EventGroupBabyEvent : FullAuditedEntity
    {
        public int GroupId { get; set; }
        public int EventId { get; set; }
        public EventGroup EventGroup { get; set; }
        public BabyEvent BabyEvent { get; set; }
    }
}
