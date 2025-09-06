

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class PlayerProfessionListDto : FullAuditedEntityDto<int>,ISearchOutModel<PlayerProfession,int>
    {

        
		/// <summary>
		/// PlayerId
		/// </summary>
		public Guid PlayerId { get; set; }



		/// <summary>
		/// FamilyId
		/// </summary>
		public int FamilyId { get; set; }



		/// <summary>
		/// ProfessionId
		/// </summary>
		public int ProfessionId { get; set; }



		/// <summary>
		/// Profession
		/// </summary>
		public Profession Profession { get; set; }



		/// <summary>
		/// Family
		/// </summary>
		public Family Family { get; set; }



		/// <summary>
		/// Player
		/// </summary>
		public Player Player { get; set; }




    }
}