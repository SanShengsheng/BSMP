using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.Feedbacks
{
    [Table("Feedbacks")]
    public class Feedback:FullAuditedEntity<Guid>
    {

        /// <summary>
        /// 玩家编号
        /// </summary>
        public virtual Guid PlayerId { get; set; }

        /// <summary>
        /// 问题编号
        /// </summary>
        public virtual int QuestionId { get; set; }

        /// <summary>
        /// 扩展字段 主题Id
        /// </summary>
        public int TopicId { get; set; }

        /// <summary>
        /// 反馈内容
        /// </summary>
        //[Required]
        public string Content { get; set; }
    }
}
