using Abp.AutoMapper;
using MQKJ.BSMP.ChineseBabies.PropMall.BabyPropPrices;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.PropMall.BabyProps.Dtos
{
    public class UpdateFamilyAssetAndBabyAwardDto
    {
        public UpdateFamilyAssetAndBabyAwardDto()
        {
            BuyProp = new PostBuyPropInput();
            Prop = new BuyBabyPropDto();
            PropPrice = new BabyPropPriceDto();
            Family = new UpdateFamilyAssetAndBabyAwardFamily();
        }

        public PostBuyPropInput BuyProp { get; set; }


        public BuyBabyPropDto Prop { get; set; }

        public BabyPropPriceDto PropPrice { get; set; }

        public UpdateFamilyAssetAndBabyAwardFamily Family { get; set; }
    }

    [AutoMapFrom(typeof(Family))]
    public class UpdateFamilyAssetAndBabyAwardFamily
    {
        public Baby LatestBaby { get; set; }

        public double Deposit { get; set; }
    }
}
