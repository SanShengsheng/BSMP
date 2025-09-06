using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Timing;
using MQKJ.BSMP.Questions;

namespace MQKJ.BSMP.Answers
{
    /// <summary>
    /// 选项表
    /// </summary>
    [Table("Answers")]
    public class Answer: FullAuditedEntity
    {
        public Answer()
        {
            CreationTime = Clock.Now;
            IsDeleted = false;
        }
        /// <summary>
        /// 问题编号
        /// </summary>
        public virtual int QuestionID { get; set; }

        /// <summary>
        /// 问题属性（男：0，女：1）
        /// </summary>
        public virtual QuestionGender QuestionType { get; set; }

        /// <summary>
        /// 问题标题
        /// </summary>
        [StringLength(72)]
        [Required]
        public virtual string Title { get; set; }

        /// <summary>
        /// 问题排序
        /// </summary>
        public virtual int Sort { get; set; }

        /// <summary>
        /// 问题（所属）
        /// </summary>
        public virtual Question Question { get; set; }

     
    }
}
