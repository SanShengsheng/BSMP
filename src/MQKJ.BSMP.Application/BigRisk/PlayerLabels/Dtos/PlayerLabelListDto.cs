

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.PlayerLabels;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.PlayerLabels.Dtos
{
    public class PlayerLabelListDto : AuditedEntityDto<Guid> 
    {

        
		/// <summary>
		/// LabelContent
		/// </summary>
		public string LabelContent { get; set; }



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