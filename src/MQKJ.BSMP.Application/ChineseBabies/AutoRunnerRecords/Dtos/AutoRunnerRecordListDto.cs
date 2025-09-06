

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class AutoRunnerRecordListDto : ISearchOutModel<AutoRunnerRecord, Guid>
    {

        
		/// <summary>
		/// FamilyId
		/// </summary>
		public int FamilyId { get; set; }



		/// <summary>
		/// PlayerId
		/// </summary>
		public Guid PlayerId { get; set; }



		/// <summary>
		/// GroupId
		/// </summary>
		public int GroupId { get; set; }



		/// <summary>
		/// ActionType
		/// </summary>
		public ActionType ActionType { get; set; }



		/// <summary>
		/// RelateionId
		/// </summary>
		public string RelateionId { get; set; }



		/// <summary>
		/// OriginalData
		/// </summary>
		public string OriginalData { get; set; }



		/// <summary>
		/// NewData
		/// </summary>
		public string NewData { get; set; }



		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; }


        public DateTime CreationTime { get; set; }
        
        [AutoMapper.IgnoreMap]
        public String CreatedTime
        {
            get
            {
                return CreationTime.AddHours(8).ToString("yyyy-MM-dd HH:mm");
            }
        }
    }
}