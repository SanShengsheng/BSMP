using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.PropMall
{
    /// <summary>
    /// 获得道具记录
    /// </summary>
    [Table("BabyPropRecords")]
    public class BabyPropRecord : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 家庭编号
        /// </summary>
        public int FamilyId { get; set; }
        /// <summary>
        /// 家庭
        /// </summary>
        public Family Family { get; set; }
        /// <summary>
        /// 购买者编号
        /// </summary>
        public Guid? PurchaserId { get; set; }
        /// <summary>
        /// 购买者
        /// </summary>
        public Player Purchaser { get; set; }
        /// <summary>
        /// 道具编号
        /// </summary>
        public int? BabyPropId { get; set; }
        /// <summary>
        /// 道具
        /// </summary>
        public BabyProp BabyProp { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public Guid? OrderId { get; set; }
        /// <summary>
        /// 道具来源
        /// </summary>
        public PropSource PropSource { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public Guid? BabyPropBagId { get; set; }

        public BabyPropBag BabyPropBag { get; set; }

        public int? BabyId { get; set; }

    }
    /// <summary>
    /// 道具来源
    /// </summary>
    public enum PropSource
    {
        /// <summary>
        /// 从商城购买
        /// </summary>
        BuyFromStore = 1,
        /// <summary>
        /// 充值金币赠送
        /// </summary>
        PresentByChargeCoin = 2,
        /// <summary>
        /// 竞技场赠送
        /// </summary>
        PresentByArena = 3,
        /// <summary>
        /// 从大礼包购买
        /// </summary>
        BuyFromPropBag = 4,

        DangradingReward = 5,
    }
}
