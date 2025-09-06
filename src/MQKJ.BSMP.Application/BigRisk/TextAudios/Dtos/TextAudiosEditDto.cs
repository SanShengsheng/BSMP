
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.CommonEnum;
using MQKJ.BSMP.TextAudios;

namespace  MQKJ.BSMP.TextAudios.Dtos
{
    public class TextAudiosEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; set; }



        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public EGender Gender { get; set; }



		/// <summary>
		/// Scene
		/// </summary>
		public ESceneType Scene { get; set; }


        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

    }
}