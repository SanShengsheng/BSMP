using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.WeChatWebUsers.Dtos
{
    public class UpdateUserStateInput
    {
        public Guid Id { get; set; }

        public string WechatAccount { get; set; }
    }
}
