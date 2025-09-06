using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.SceneTypes
{
    [Table("SceneTypes")]
    public class SceneType:FullAuditedEntity
    {

        /// <summary>
        /// 类型名字
        /// </summary>
        [Required]
        [StringLength(30)]
        public string TypeName { get; set; }

        public SceneType()
        {
            CreationTime = DateTime.Now;
        }
    }
}
