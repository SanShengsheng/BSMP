using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.SceneFiles;
using MQKJ.BSMP.Scenes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MQKJ.BSMP.Scenes.Dto
{
    [AutoMapFrom(typeof(Scene))]
    public class GetScenesListDto: FullAuditedEntityDto
    {
        /// <summary>
        /// 场景名字
        /// </summary>
        [Required]
        public string SceneName { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        public string Description { get; set; }

        public string SceneImageSrc { get; set; }

        public string FilePath { get; set; }

        public SceneFile SceneFile { get; set; }

    }
}
