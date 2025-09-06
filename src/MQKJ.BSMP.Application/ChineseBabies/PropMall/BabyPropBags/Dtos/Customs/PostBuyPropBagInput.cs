using System;

namespace MQKJ.BSMP.ChineseBabies.PropMall
{
    public class PostBuyPropBagInput
    {
        public int BabyId { get; set; }

        public Guid PlayerGuid { get; set; }

        public int FamilyId { get; set; }

        public Guid propBagId { get; set; }

        public CurrencyType CurrencyType { get; set; } = CurrencyType.Coin;

        public Guid? OrderId { get; set; }

        //public string Code { get; set; }
    }
}