using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.PropMall.BabyProps.Dtos
{
    [AutoMapFrom(typeof(BabyProp))]
    public class BuyBabyPropDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MaxPurchasesNumber { get; set; }

        public BabyPropPropertyAward BabyPropPropertyAward { get; set; }


        public BuyBabyPropTypeDto BabyPropType { get; set; }
    }

    [AutoMapFrom(typeof(BabyPropType))]
    public class BuyBabyPropTypeDto
    {
        /// <summary>
        /// 装备对象
        /// </summary>
        public EquipmentAbleObject EquipmentAbleObject { get; set; }
    }
}
