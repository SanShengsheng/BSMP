

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.Players.Dtos
{
    public class CreateOrUpdatePlayerInput
    {
        [Required]
        public PlayerEditDto Player { get; set; }

    }
}