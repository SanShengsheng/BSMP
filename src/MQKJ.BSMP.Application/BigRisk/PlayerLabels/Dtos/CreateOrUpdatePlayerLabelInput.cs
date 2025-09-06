

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.PlayerLabels;

namespace MQKJ.BSMP.PlayerLabels.Dtos
{
    public class CreateOrUpdatePlayerLabelInput
    {
        [Required]
        public PlayerLabelEditDto PlayerLabel { get; set; }

    }
}