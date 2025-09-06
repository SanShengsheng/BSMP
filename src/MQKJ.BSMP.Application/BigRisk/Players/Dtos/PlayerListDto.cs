

using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.PlayerDramas;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.StoryLines;

namespace MQKJ.BSMP.Players.Dtos
{
    public class PlayerListDto : FullAuditedEntityDto<Guid>,IMustHaveTenant 
    {

        
		/// <summary>
		/// TenantId
		/// </summary>
		public int TenantId { get; set; }



		/// <summary>
		/// NickName
		/// </summary>
		public string NickName { get; set; }



		/// <summary>
		/// HeadUrl
		/// </summary>
		public string HeadUrl { get; set; }



		/// <summary>
		/// Gender
		/// </summary>
		public int Gender { get; set; }



		/// <summary>
		/// AgeRange
		/// </summary>
		public int AgeRange { get; set; }



		/// <summary>
		/// OpenId
		/// </summary>
		public string OpenId { get; set; }



		/// <summary>
		/// ModifyCount
		/// </summary>
		public int ModifyCount { get; set; }



		/// <summary>
		/// AuthorizeDateTime
		/// </summary>
		public DateTime AuthorizeDateTime { get; set; }



		/// <summary>
		/// State
		/// </summary>
		public int State { get; set; }



		/// <summary>
		/// IsDeveloper
		/// </summary>
		public bool IsDeveloper { get; set; }



		/// <summary>
		/// PlayerExtension
		/// </summary>
		public PlayerExtension PlayerExtension { get; set; }



		/// <summary>
		/// PlayerExtensionId
		/// </summary>
		public int? PlayerExtensionId { get; set; }



		/// <summary>
		/// GameTaskInvitees
		/// </summary>
		public ICollection<GameTask> GameTaskInvitees { get; set; }



		/// <summary>
		/// GameTaskInviters
		/// </summary>
		public ICollection<GameTask> GameTaskInviters { get; set; }



		/// <summary>
		/// PlayerDramas
		/// </summary>
		public ICollection<PlayerDrama> PlayerDramas { get; set; }



		/// <summary>
		/// StoryLinesPlayerA
		/// </summary>
		public ICollection<StoryLine> StoryLinesPlayerA { get; set; }



		/// <summary>
		/// StoryLinesPlayerB
		/// </summary>
		public ICollection<StoryLine> StoryLinesPlayerB { get; set; }




    }
}