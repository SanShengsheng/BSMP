using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.SceneFiles;
using MQKJ.BSMP.SceneTypes;

namespace MQKJ.BSMP.Scenes
{
    [Table("Scenes")]
    public class Scene: FullAuditedEntity
    {

        /// <summary>
        /// 场景名字
        /// </summary>
        [Required]
        [StringLength(16)]
        [MaxLength(BSMPConsts.MaxSceneNameLength)]
        public string SceneName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Sort { get; set; }

        /// <summary>
        /// 默认图片的Id
        /// </summary>
        public virtual Guid DefaultSceneFileId { get; set; }

        /// <summary>
        /// 场景类型Id
        /// </summary>
        [Required]
        [DefaultValue(1)]
        public int SceneTypeId { get; set; }

        /// <summary>
        /// 场景类型
        /// </summary>
        [ForeignKey("SceneTypeId")]
        public virtual SceneType Type { get; set; }

        /// <summary>
        /// 场景描述
        /// </summary>
        [MaxLength(BSMPConsts.MaxSceneDescriptionLength)]
        public string Description { get; set; }

        public SceneFile SceneFile { get; set; }

        public Scene()
        {
            CreationTime = DateTime.Now;
        }

    }
}
