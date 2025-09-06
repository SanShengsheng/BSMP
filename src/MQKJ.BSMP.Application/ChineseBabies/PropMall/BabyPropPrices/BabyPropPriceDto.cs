using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.PropMall.BabyPropPrices
{
    [AutoMapFrom(typeof(BabyPropPrice))]
    public class BabyPropPriceDto
    {
        public int Id { get; set; }

        public double Price { get; set; }
        public double Validity { get; set; }
    }
}
