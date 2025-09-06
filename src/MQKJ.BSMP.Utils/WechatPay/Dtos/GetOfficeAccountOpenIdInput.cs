using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Utils.WechatPay.Dtos
{
    public class GetOfficeAccountOpenIdInput
    {
        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string Code { get; set; }
    }
}
