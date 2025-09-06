

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class EventGroupBabyEventListDto : FullAuditedEntityDto 
    {

        
		/// <summary>
		/// GroupId
		/// </summary>
		public int GroupId { get; set; }



		/// <summary>
		/// EventId
		/// </summary>
		public int EventId { get; set; }



		/// <summary>
		/// EventGroup
		/// </summary>
		public EventGroup EventGroup { get; set; }



		/// <summary>
		/// BabyEvent
		/// </summary>
		public BabyEvent BabyEvent { get; set; }




    }
}