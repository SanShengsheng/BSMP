

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class EventGroupListDto : FullAuditedEntityDto<int> , ISearchOutModel<EventGroup, int>
    {

        
		/// <summary>
		/// PrevGroupId
		/// </summary>
		public int? PrevGroupId { get; set; }



		/// <summary>
		/// PrevGroup
		/// </summary>
		public EventGroup PrevGroup { get; set; }



		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; }



		/// <summary>
		/// GroupEvents
		/// </summary>
        [NotMapped]
		public IEnumerable<EventGroupBabyEvent> GroupEvents { get; set; }




    }
}