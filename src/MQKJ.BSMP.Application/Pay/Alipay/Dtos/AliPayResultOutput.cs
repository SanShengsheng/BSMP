using System.Threading.Tasks;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;

namespace MQKJ.BSMP.Alipay
{
    public class AliPayResultOutput
    {
        public Family Family { get; set; }
        public CoinRechargeRecord CoinRechargeRecord { get; set; }
        public int GoldCoin { get; internal set; }
    }
}