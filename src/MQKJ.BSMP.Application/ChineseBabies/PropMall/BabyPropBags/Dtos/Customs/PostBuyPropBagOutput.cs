using MQKJ.BSMP.Utils.WechatPay.Dtos;
using System.Collections.Generic;

namespace MQKJ.BSMP.ChineseBabies.PropMall
{
    public class PostBuyPropBagOutput
    {
        public PostBuyPropBagOutput()
        {
            PayOutput = new MiniProgramPayOutput();
        }

        public ICollection<string> Messages { get; set; } = new List<string>();

        public MiniProgramPayOutput PayOutput { get; set; }
    }
}