

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class BabyPropTypeListDto : FullAuditedEntityDto ,ISearchOutModel<BabyPropType,int>
    {

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        public  string Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }



		/// <summary>
		/// Img
		/// </summary>
		public string Img { get; set; }



		/// <summary>
		/// Code
		/// </summary>
		public int Code { get; set; }



		/// <summary>
		/// Sort
		/// </summary>
		public int Sort { get; set; }




    }
}