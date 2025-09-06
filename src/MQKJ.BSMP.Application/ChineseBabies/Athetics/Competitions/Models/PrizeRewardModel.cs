using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Models
{
    [AutoMapFrom(typeof(BabyProp))]
    public class PrizeRewardModel
    {
        public string Title { get; set; }

        public PropLevel Level { get; set; }


        public int Code { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string CoverImg { get; set; }

        /// <summary>
        /// 道具类型
        /// </summary>
        public PrizeRewardBabyPropType BabyPropType { get; set; }

        /// <summary>
        /// 折成金币数量
        /// </summary>
        public double PrizeCoinCount { get; set; }
    }

    [AutoMapFrom(typeof(BabyPropType))]
    public class PrizeRewardBabyPropType
    {
        public string Img { get; set; }

        public int Code { get; set; }

        public int Sort { get; set; }

        public string Name { get; set; }
        /// <summary>
        /// 装备对象
        /// </summary>
        public EquipmentAbleObject EquipmentAbleObject { get; set; }
        /// <summary>
        /// 最大可装备数量
        /// </summary>
        public bool MaxEquipmentCount { get; set; }
    }
}
