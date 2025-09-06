using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class RegisterSignUpSystemInput
    {
        public string UserName { get; set; }

        public int Age { get; set; }

        public int Gender { get; set; }

        public string Profession { get; set; }

        public string City { get; set; }

        public string Interest { get; set; }

        public long? UserId { get; set; }
    }
}
