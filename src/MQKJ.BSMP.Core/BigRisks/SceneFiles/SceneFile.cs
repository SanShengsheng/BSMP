using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.BSMPFiles;
using MQKJ.BSMP.Scenes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.SceneFiles
{
    [Table("SceneFiles")]
    public class SceneFile : FullAuditedEntity<Guid>
    {

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 场景Id
        /// </summary>
        [Required]
        public int SceneId { get; set; }

        /// <summary>
        /// 场景实体
        /// </summary>
        [ForeignKey("SceneId")]
        public virtual Scene Scene { get; set; }

        /// <summary>
        /// 文件Id
        /// </summary>
        public Guid FileId { get; set; }

        /// <summary>
        /// 是否是默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 场景文件名称
        /// </summary>
        public string SceneFileName { get; set; }

        /// <summary>
        /// 文件实体
        /// </summary>
        [ForeignKey("FileId")]
        public BSMPFile File { get; set; }
    }
}
