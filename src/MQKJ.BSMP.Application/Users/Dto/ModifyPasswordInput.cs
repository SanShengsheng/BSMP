using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Users.Dto
{
    public class ModifyPasswordInput
    {
        public long UserId { get; set; }

        public string OldPassword { get; set; }


        public string NewPassword { get; set; }
    }
}
