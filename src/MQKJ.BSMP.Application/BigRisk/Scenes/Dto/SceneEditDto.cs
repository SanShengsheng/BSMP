using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using MQKJ.BSMP.Scenes;
using System.ComponentModel;
using Abp.Domain.Entities.Auditing;
using Abp.Application.Services.Dto;

namespace MQKJ.BSMP.Scenes.Dto
{
    [AutoMapTo(typeof(Scene))]
    public class SceneEditDto
    {

        public int? Id { get; set; }
        /// <summary>
        /// 场景名字
        /// </summary>
        [Required]
        public string SceneName { get; set; }

        //[Required]
        public int SceneTypeId { get;set; }

        public string Description { get; set; }

    }
}
