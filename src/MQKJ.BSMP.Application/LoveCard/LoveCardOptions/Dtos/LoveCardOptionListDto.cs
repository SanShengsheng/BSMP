

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.LoveCardOptions;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.LoveCards;

namespace MQKJ.BSMP.LoveCardOptions.Dtos
{
    public class LoveCardOptionListDto : AuditedEntityDto<Guid> 
    {

		/// <summary>
		/// LoveCardId
		/// </summary>
		public Guid LoveCardId { get; set; }



		/// <summary>
		/// OptionPlayer
		/// </summary>
		public Player OptionPlayer { get; set; }



        public bool IsLiked { get; set; }



        /// <summary>
        /// OptionPlayerId
        /// </summary>
        public Guid OptionPlayerId { get; set; }



		/// <summary>
		/// LoveCardOptionType
		/// </summary>
		public LoveCardOptionType LoveCardOptionType { get; set; }




    }
}