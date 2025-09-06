using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.BonusPoints
{
    [Table("BonusPoints")]
    public class BonusPoint:FullAuditedEntity
    {

        /// <summary>
        /// 爱豆数量
        /// </summary>
        public virtual int PointsCount { get; set; }

        /// <summary>
        /// 事件名称
        /// </summary>
        //[Required]
        public virtual string EventName { get; set; }

        /// <summary>
        /// 事件描述
        /// </summary>

        public virtual string EventDescription { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Code { get; set; }
    }
}
