using Abp.AutoMapper;
using MQKJ.BSMP.ChineseBabies.PropMall;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MQKJ.BSMP.ChineseBabies
{
    public class GetBabyPropsOutput
    {
        public GetBabyPropsOutputBasicInfo BasicInfo { get; set; }

        public GetBabyPropsOutputDetail Detail { get; set; }
    }

    public class GetBabyPropsOutputDetail
    {
        /// <summary>
        /// 条件集合
        /// </summary>
        public ICollection<GetBabyPropsOutputDetailTerm> Terms { get; set; }
        /// <summary>
        /// 功能集合
        /// </summary>
        public ICollection<GetBabyPropsOutputDetailFeature> Features { get; set; }
        /// <summary>
        /// 附加属性
        /// </summary>
        public GetBabyPropsOutputDetailPropertyAddition PropertyAddition { get; set; }
    }

    public class GetBabyPropsOutputDetailPropertyAddition
    {
        /// <summary>
        /// 事件加成方式
        /// </summary>
        public EventAdditionType EventAdditionType { get; set; }
        /// <summary>
        /// 智力
        /// </summary>
        public virtual int Intelligence { get; set; }
        /// <summary>
        /// 体魄
        /// </summary>
        public virtual int Physique { get; set; }
        /// <summary>
        /// 想象
        /// </summary>
        public virtual int Imagine { get; set; }
        /// <summary>
        /// 意志
        /// </summary>
        public virtual int WillPower { get; set; }
        /// <summary>
        /// 情商
        /// </summary>
        public virtual int EmotionQuotient { get; set; }
        /// <summary>
        /// 魅力
        /// </summary>
        public virtual int Charm { get; set; }
    }

    public class GetBabyPropsOutputDetailFeature
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        public int Id { get; set; }
        /// <summary>
        /// 特性类别
        /// </summary>
        public FeatureType Type { get; set; }
    }

    public class GetBabyPropsOutputDetailTerm
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        public int Id { get; set; }

        public Double MinValue { get; set; }

        public Double MaxValue { get; set; }
    }

    public class GetBabyPropsOutputBasicInfo
    {
        public int Code { get; set; }

        public int Id { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// CoverImg
        /// </summary>
        public string CoverImg { get; set; }
        private ICollection<GetBabyPropsOutputBasicInfo_Prices> _prices { get; set; }
        public ICollection<GetBabyPropsOutputBasicInfo_Prices> Prices
        {
            get => _prices.OrderBy(s => s.Price).ToList();
            set => _prices = value;
        }
        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }

    }
    //[AutoMapFrom(typeof(BabyPropPrice))]
    public class GetBabyPropsOutputBasicInfo_Prices
    {
        public int Id { get; set; }
        /// <summary>
        /// 货币类型
        /// </summary>
        public CurrencyType CurrencyType { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        public int Sort { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public double Validity { get; set; }

        public bool IsDefault { get; set; }
    }
}