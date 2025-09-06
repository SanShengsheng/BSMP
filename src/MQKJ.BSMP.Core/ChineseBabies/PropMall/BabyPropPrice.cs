using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies.PropMall;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 商品价格表
    /// </summary>
    [Table("BabyPropPrices")]
    public class BabyPropPrice : FullAuditedEntity
    {
        /// <summary>
        /// 货币类型
        /// </summary>
        public CurrencyType CurrencyType { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        public BabyProp BabyProp { get; set; }

        public int BabyPropId { get; set; }
        /// <summary>
        /// 道具价值 （资产值）
        /// </summary>
        public double PropValue { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Dsecription { get; set; }

        public int Sort { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public double  Validity { get; set; }

        public bool IsDefault { get; set; }

        //public Guid? BabyPropBagId { get; set; }

        //public BabyPropBag BabyPropBag { get; set; }
    }


    public enum CurrencyType
    {
        /// <summary>
        /// 金币
        /// </summary>
        [EnumHelper.EnumDescription("金币")]
        Coin = 1,

        /// <summary>
        /// 人民币
        /// </summary>
        [EnumHelper.EnumDescription("人民币")]
        RMB = 2,

    }
}
