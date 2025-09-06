using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies.PropMall;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.ChineseBabies
{
    [Table("BabyProps")]
    public class BabyProp : FullAuditedEntity
    {
        /// <summary>
        /// 道具标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 是否是默认
        /// </summary>
        public bool IsDefault { get; set; }

        public Guid? BabyPropPropertyAwardId { get; set; }
        /// <summary>
        /// 属性加成
        /// </summary>
        public BabyPropPropertyAward BabyPropPropertyAward { get; set; }

        /// <summary>
        /// 道具等级
        /// </summary>
        public PropLevel Level { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        public double Discount { get; set; }

        /// <summary>
        /// 折扣信息
        /// </summary>
        public string DiscountInfo { get; set; }

        /// <summary>
        /// 是否是新品
        /// </summary>
        public bool IsNewProp { get; set; }

        /// <summary>
        /// 购买次数
        /// </summary>
        public int MaxPurchasesNumber { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string CoverImg { get; set; }

        public int? BabyPropTypeId { get; set; }
        /// <summary>
        /// 道具类型
        /// </summary>
        public BabyPropType BabyPropType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否购买后播放跑马灯信息
        /// </summary>
        public bool IsAfterBuyPlayMarquees { get; set; }

        /// <summary>
        /// 获得道具的途径
        /// </summary>
        public GetWay GetWay { get; set; }
        /// <summary>
        /// 道具性别
        /// </summary>
        public Gender? Gender { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 触发显示的道具
        /// </summary>
        public int? TriggerShowPropCode { get; set; }
        /// <summary>
        /// 触发显示的道具（第二个，目前仅在皮肤下有用到）
        /// </summary>
        public int? TriggerNextShowPropCode { get; set; }

        /// <summary>
        /// 是否可继承
        /// </summary>
        public bool IsInheritAble { get; set; }
        /// <summary>
        /// 价格集合
        /// </summary>
        public virtual ICollection<BabyPropPrice> Prices { get; set; }

        /// <summary>
        /// 条件集合
        /// </summary>
        public virtual ICollection<BabyPropTerm> BabyPropTerms { get; set; }

        /// <summary>
        /// 功能集合
        /// </summary>
        public virtual ICollection<BabyPropFeature> BabyPropFeatures { get; set; }

    }
    /// <summary>
    /// 道具等级
    /// </summary>
    public enum PropLevel
    {
        First = 1,

        Second = 2,

        Third = 3,

        Fourth = 4,

        Fifth = 5,

        Sixth = 6,

        Seventh = 7,

        Eighth = 8,

        Ninth = 9,

        Tenth = 10,

        Eleventh = 11,

        Twelfth = 12


    }
    /// <summary>
    /// 获得方式
    /// </summary>
    public enum GetWay
    {
        /// <summary>
        /// 无限制
        /// </summary>
        Any = 0,
        /// <summary>
        /// 商店
        /// </summary>
        Store = 1,
        /// <summary>
        /// 竞技场
        /// </summary>
        Arena = 2,
    }

}
