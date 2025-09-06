
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Players;

namespace  MQKJ.BSMP.Players.Dtos
{
    public class PlayerEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
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
		//public string OpenId { get; set; }



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



		///// <summary>
		///// GameTaskInvitees
		///// </summary>
		//public ICollection<GameTask> GameTaskInvitees { get; set; }



		///// <summary>
		///// GameTaskInviters
		///// </summary>
		//public ICollection<GameTask> GameTaskInviters { get; set; }



		///// <summary>
		///// PlayerDramas
		///// </summary>
		//public ICollection<PlayerDrama> PlayerDramas { get; set; }



		///// <summary>
		///// StoryLinesPlayerA
		///// </summary>
		//public ICollection<StoryLine> StoryLinesPlayerA { get; set; }



		///// <summary>
		///// StoryLinesPlayerB
		///// </summary>
		//public ICollection<StoryLine> StoryLinesPlayerB { get; set; }




    }
}