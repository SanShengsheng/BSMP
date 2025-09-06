

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.BabyPropertyBaseDtos;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class BabyListDto : BabyPropertyBase<int>,ISearchOutModel<Baby,int>
    {

        
		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }



		/// <summary>
		/// Gender
		/// </summary>
		public Gender Gender { get; set; }



		/// <summary>
		/// FamilyId
		/// </summary>
		public int FamilyId { get; set; }



		/// <summary>
		/// CoverImage
		/// </summary>
		public string CoverImage { get; set; }



		/// <summary>
		/// State
		/// </summary>
		public BabyState State { get; set; }



		/// <summary>
		/// BabyEndingId
		/// </summary>
		public int? BabyEndingId { get; set; }



		/// <summary>
		/// BabyEnding
		/// </summary>
		public BabyEnding BabyEnding { get; set; }

        public int? GroupId { get; set; }
        public EventGroup Group { get; set; }

        /// <summary>
        /// 爸爸是否查看宝宝出生动画
        /// </summary>
        public bool IsWatchBirthMovieFather { get; set; }
        /// <summary>
        /// 妈妈是否查看宝宝出生动画
        /// </summary>
        public bool IsLoadBirthMovieMother { get; set; }
    }
}