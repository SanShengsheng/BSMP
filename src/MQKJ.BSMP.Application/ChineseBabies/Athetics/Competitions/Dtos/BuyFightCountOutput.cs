using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos
{
    public class BuyFightCountOutput
    {
        public double CoinCount { get; set; }

        public BuyFightCountErrorCode ErrorCode { get; set; }

    }

    public enum BuyFightCountErrorCode
    {
        NotEnough = 1,

        BuyFightCountLimit = 2,

        Fail = 3
    }
}
