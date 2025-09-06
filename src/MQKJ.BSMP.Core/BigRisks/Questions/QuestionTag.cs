using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;
using MQKJ.BSMP.Tags;


namespace MQKJ.BSMP.Questions
{
    [Table("QuestionTags")]
    public class QuestionTag : FullAuditedEntity
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public virtual int Id { get; set; }
        /// <summary>
        /// 问题编号
        /// </summary>
        public virtual int QuestionId { get; set; }

        /// <summary>
        /// 标签编号
        /// </summary>
        public virtual int TagId { get; set; }

        /// <summary>
        /// 问题
        /// </summary>
        public virtual Question Question { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public virtual Tag Tag { get; set; }



    }
}
