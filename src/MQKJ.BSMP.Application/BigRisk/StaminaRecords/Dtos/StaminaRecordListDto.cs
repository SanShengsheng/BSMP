

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.StaminaRecords;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.StaminaRecords.Dtos
{
    public class StaminaRecordListDto : FullAuditedEntityDto 
    {

        
		/// <summary>
		/// StaminaCount
		/// </summary>
		public int StaminaCount { get; set; }



		/// <summary>
		/// PlayerId
		/// </summary>
		public Guid PlayerId { get; set; }



		/// <summary>
		/// Player
		/// </summary>
		public Player Player { get; set; }

    }
}