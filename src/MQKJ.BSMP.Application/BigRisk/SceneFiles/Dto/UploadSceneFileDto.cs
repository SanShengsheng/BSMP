using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Microsoft.AspNetCore.Http;
using MQKJ.BSMP.SceneFiles.Dto;
using MQKJ.BSMP.BSMPFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MQKJ.BSMP.SceneFiles.SceneFiles.Dto
{
    [AutoMapTo(typeof(BSMPFile))]
    public class UploadSceneFileDto : EntityDto<Guid>
    {
        [Required]
        public IFormFile FormFile { get; set; }

        /// <summary>
        /// 场景Id
        /// </summary>
        public int SceneId { get; set; }

        /// <summary>
        /// 场景文件名
        /// </summary>
        public string SceneFileName { get; set; }

        /// <summary>
        /// 上传文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public double Size { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string NewFileName { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType Type { get; set; }

        //public SceneFileDto SceneFileInput { get; set; }
    }
}
