

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Likes;

namespace MQKJ.BSMP.Likes.Dtos
{
    public class CreateOrUpdateLikeInput
    {
        [Required]
        public LikeEditDto Like { get; set; }

    }
}