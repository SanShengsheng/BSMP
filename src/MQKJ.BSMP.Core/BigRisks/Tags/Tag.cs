using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Questions;
using MQKJ.BSMP.TagTypes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.Tags
{
    [Table("Tags")]
    public class Tag : FullAuditedEntity
    {
        /// <summary>
        /// 标签类别编号
        /// </summary>
        public virtual int TagTypeId {get;set;}

        /// <summary>
        /// 标签名
        /// </summary>
        [StringLength(50)]
        public virtual string TagName { get; set; }

        /// <summary>
        /// 排序，大到小
        /// </summary>
        public virtual int Sort { get; set; }

        /// <summary>
        /// 标签类型
        /// </summary>
        public  virtual  TagType TagType { get; set; }

        /// <summary>
        /// 问题集合
        /// </summary>
       public  virtual  ICollection<QuestionTag> QuestionTags { get; set; } 

    }
}
