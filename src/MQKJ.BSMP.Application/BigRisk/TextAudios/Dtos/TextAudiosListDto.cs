

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.CommonEnum;

namespace MQKJ.BSMP.TextAudios.Dtos
{
    public class TextAudiosListDto : FullAuditedEntityDto 
    {

        
		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; set; }



		/// <summary>
		/// Gender
		/// </summary>
		public EGender Gender { get; set; }



		/// <summary>
		/// Scene
		/// </summary>
		public ESceneType Scene { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

    }
}