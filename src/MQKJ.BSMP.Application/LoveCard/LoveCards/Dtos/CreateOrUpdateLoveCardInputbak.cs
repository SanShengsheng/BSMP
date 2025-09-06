

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.LoveCards;

namespace MQKJ.BSMP.LoveCards.Dtos
{
    public class CreateOrUpdateLoveCardInputbak
    {
        [Required]
        public LoveCardEditDto LoveCard { get; set; }

    }
}