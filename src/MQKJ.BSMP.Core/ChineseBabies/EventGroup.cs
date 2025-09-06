using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 事件表
    /// </summary>
    [Table("EventGroups")]
    public class EventGroup : FullAuditedEntity<int>
    {
        /// <summary>
        /// 前置事件组Id
        /// </summary>
        public int? PrevGroupId { get; set; }
        public EventGroup PrevGroup { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 编码，导入数据使用
        /// </summary>
        public int? Code { get; set; }
        [NotMapped]
        public virtual  IEnumerable<EventGroupBabyEvent> EventGroupBabyEvent { get; set; }
        [NotMapped]
        public  IEnumerable<BabyEvent> Events
        {
            get
            {
                return EventGroupBabyEvent?.Select(s => s.BabyEvent);
            }
        }
    }
}
