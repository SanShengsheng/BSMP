using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.PropMall
{
    /// <summary>
    /// 道具大礼包（套餐）
    /// </summary>
    [Table("BabyPropBags")]
    public class BabyPropBag : FullAuditedEntity<Guid>
    {
        public string Title { get; set; }

        public int Code { get; set; }

        public string Description { get; set; }

        

        public virtual ICollection<BabyPropBagAndBabyProp> BabyPropBagAndBabyProps { get; set; }

        public Gender Gender { get; set; }
        /// <summary>
        /// 货币数量，比如赠送1000金币
        /// </summary>
        public double CurrencyCount { get; set; }
        /// <summary>
        /// 货币类型(赠送)
        /// </summary>
        public CurrencyTypeEnum CurrencyType { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// 价格（人民币）
        /// </summary>
        public decimal PriceRMB { get; set; }
        /// <summary>
        /// 价格（金币）
        /// </summary>
        public double PriceGoldCoin { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsDisabled { get; set; }

    }

    public enum CurrencyTypeEnum {
        /// <summary>
        /// 金币
        /// </summary>
         GoldCoin=1
    }
}
