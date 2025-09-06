using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.BigRisk.Players.Dtos
{
    public class WechatPlayerInfoRequest
    {
        public Guid Id { get; set; }
        public string NickName { get; set; }
        public string HeadUrl { get; set; }
        public int Gender { get; set; }
    }
}
