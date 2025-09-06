

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Friends;

namespace MQKJ.BSMP.Friends.Dtos
{
    public class CreateOrUpdateFriendInput
    {
        [Required]
        public FriendEditDto Friend { get; set; }

    }
}