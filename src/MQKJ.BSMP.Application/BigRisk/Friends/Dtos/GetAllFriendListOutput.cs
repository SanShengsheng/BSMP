using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Friends.Dtos
{
    public class GetAllFriendListOutput
    {
        public Guid Id { get; set; }

        public string InviterName { get; set; }


        public string InviteeName { get; set; }

        public int Floor { get; set; }
    }
}
