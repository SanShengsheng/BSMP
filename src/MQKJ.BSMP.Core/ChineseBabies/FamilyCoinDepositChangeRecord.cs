using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    [Table("FamilyCoinDepositChangeRecords")]
  public  class FamilyCoinDepositChangeRecord : FullAuditedEntity<Guid>
    {
        public Guid? StakeholderId { get; set; }
        /// <summary>
        /// 干系人（消费者或者生产者）
        /// </summary>
        public Player Stakeholder { get; set; }
        /// <summary>
        /// 金币数量
        /// </summary>
        public double Amount { get; set; }
        public int? BabyId { get; set; }

        public Baby Baby { get; set; }
        /// <summary>
        /// 家庭Id
        /// </summary>
        public int FamilyId { get; set; }

        public Family Family { get; set; }
        /// <summary>
        /// 花费类型
        /// </summary>
        public CoinCostType CostType { get; set; }
        /// <summary>
        /// 获得类型
        /// </summary>
        public  CoinGetWay GetWay { get; set; }
        /// <summary>
        /// 当前家庭金币数量
        /// </summary>
        public double  CurrentFamilyCoinDeposit { get; set; }

        /// <summary>
        /// 消费金币的物品Id(比如道具id)
        /// </summary>
        public string GoodsId { get; set; }
    }
    /// <summary>
    /// 花费类型
    /// </summary>
    public enum CoinCostType
    {
        GrowUpEvent=1,

        StudyEvent=2,

        BuyProp=3,

        ChangeProfession=4,

        BuyPKCount=5,
        /// <summary>
        /// 购买道具大礼包
        /// </summary>
        BuyPropBag=6,

        /// <summary>
        /// 段位升级奖励
        /// </summary>
        DangradingUpgradeReward = 7
    }

    public enum CoinGetWay
    {
        /// <summary>
        /// 系统赠送
        /// </summary>
        SystemPresent=1,
        /// <summary>
        /// 道具折成金币
        /// </summary>
        PropToCoin=2,
        /// <summary>
        /// 虚拟充值
        /// </summary>
        virtualRecharge = 3,
    }
}
