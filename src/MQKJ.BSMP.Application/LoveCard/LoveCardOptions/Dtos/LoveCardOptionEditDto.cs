
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.LoveCardOptions;
using MQKJ.BSMP.LoveCards;
using MQKJ.BSMP.Players;

namespace  MQKJ.BSMP.LoveCardOptions.Dtos
{
    public class LoveCardOptionEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// LoveCard
		/// </summary>
		public LoveCards.LoveCard LoveCard { get; set; }



		/// <summary>
		/// LoveCardId
		/// </summary>
		public Guid LoveCardId { get; set; }



		/// <summary>
		/// OptionPlayer
		/// </summary>
		public Player OptionPlayer { get; set; }



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