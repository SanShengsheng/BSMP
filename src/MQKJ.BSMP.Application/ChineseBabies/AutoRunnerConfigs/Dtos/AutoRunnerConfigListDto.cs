

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class AutoRunnerConfigListDto :ISearchOutModel<AutoRunnerConfig, Int32>
    {

        
		/// <summary>
		/// FamilyId
		/// </summary>
		public int FamilyId { get; set; }



		/// <summary>
		/// PlayerId
		/// </summary>
		public Guid PlayerId { get; set; }



        public ConsumeLevel ConsumeLevel { get; set; }
        public ConsumeLevel StudyLevel { get; set; }
        public LevelAction LevelAction { get; set; }
        public int BuyCount { get; set; }
    }
}