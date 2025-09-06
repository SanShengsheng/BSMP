using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MQKJ.BSMP.Tags;

namespace MQKJ.BSMP.TagTypes
{
    [Table("TagTypes")]
    public class TagType :FullAuditedEntity
    {
        /// <summary>
        /// 类型名
        /// </summary>
        [StringLength(50)]
        public virtual string TypeName { get; set; }

        /// <summary>
        /// 标签集合
        /// </summary>
        public virtual  ICollection<Tag> Tags { get; set; }

        public TagType()
        {

        }
        public TagType(string _tagName,string _code)
        {
            TypeName = _tagName;
            Code = _code;
        }

        /// <summary>
        /// 类型代码，方便查询
        /// </summary>
        public string Code
        {
            get;set;
        }
    }
}
