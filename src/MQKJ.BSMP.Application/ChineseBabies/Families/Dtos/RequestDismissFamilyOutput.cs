using MQKJ.BSMP.Utils.WechatPay.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Dtos
{
    public class RequestDismissFamilyOutput
    {
        public RequestDismissFamilyOutput()
        {
            PayOutput = new MiniProgramPayOutput();
        }

        public MiniProgramPayOutput PayOutput { get; set; }
    }
}
