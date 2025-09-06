using MQKJ.BSMP.LoveCardOptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.LoveCards.Dtos
{
    public class GetLikeAndSharePlayerListOutput
    {
        /// <summary>
        /// LoveCardId
        /// </summary>
        //public Guid LoveCardId { get; set; }



        /// <summary>
        /// OptionPlayer
        /// </summary>
        public CardOptionPlayerDto OptionPlayer { get; set; }



        //public bool IsLiked { get; set; }



        /// <summary>
        /// OptionPlayerId
        /// </summary>
        //public Guid OptionPlayerId { get; set; }



        /// <summary>
        /// LoveCardOptionType
        /// </summary>
        public LoveCardOptionType LoveCardOptionType { get; set; }
    }

    public class CardOptionPlayerDto
    {
        public string HeadUrl { get; set; }
    }
}
