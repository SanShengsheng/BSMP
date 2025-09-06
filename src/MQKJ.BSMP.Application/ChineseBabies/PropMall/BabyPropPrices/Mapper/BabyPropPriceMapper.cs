using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.PropMall.BabyPropPrices.Mapper
{
    public class BabyPropPriceMapper : Profile
    {
        public BabyPropPriceMapper()
        {
            CreateMap<BabyPropPrice, GetBabyPropsOutputBasicInfo_Prices>();
        }
    }
}
