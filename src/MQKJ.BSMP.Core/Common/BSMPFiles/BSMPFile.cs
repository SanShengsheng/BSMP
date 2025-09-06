using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MQKJ.BSMP.Questions;
using MQKJ.BSMP.SceneFiles;

namespace MQKJ.BSMP.BSMPFiles
{
    [Table("BSMPFiles")]
    public class BSMPFile : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        [Column(TypeName = "VARCHAR(255)")]
        public string FilePath { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public double Size { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        [Required]
        public FileType type { get; set; }

        /// <summary>
        /// 原文件名
        /// </summary>
        [Column(TypeName = "VARCHAR(255)")]
        public string FileName { get; set; }

        /// <summary>
        /// 新文件名
        /// </summary>
        [Column(TypeName = "VARCHAR(255)")]
        public string NewFileName { get; set; }

        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string ThumbnailImagePath { get; set; }


        public  virtual  SceneFile SceneFile { get; set; }

        public virtual  ICollection<Question> Questions { get; set; }
        public BSMPFile()
        {
            CreationTime = DateTime.Now;
        }

    }
}
