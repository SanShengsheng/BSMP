using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Common.WechatPay;
using MQKJ.BSMP.EnumHelper;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Products;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.Orders
{
    [Table("Orders")]
    public class Order : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 付款金额(RMB)
        /// </summary>
        public virtual double Payment { get; set; }

        /// <summary>
        /// 应结订单金额=订单金额-非充值代金券金额，应结订单金额< 订单金额。
        /// </summary>
        public double SettlementTotalFee { get; set; }

        /// <summary>
        /// 邮费
        /// </summary>
        //public virtual int PostFee { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public virtual string ProductDescribe { get; set; }

        /// <summary>
        /// 付款类型
        /// </summary>
        public virtual PaymentType PaymentType { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public virtual string OrderNumber { get; set; }

        /// <summary>
        /// 交易订单号
        /// </summary>
        public virtual string TransactionId { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        public virtual DateTime? PaymentTime { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public virtual OrderState State { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual Guid? PlayerId { get; set; }

        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }

        /// <summary>
        /// 产品Id 商品Id（充值金币Id、职业花费Id）
        /// </summary>
        public virtual int? ProductId { get; set; }

        public int? FamilyId { get; set; }

        public Family Family { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        public GoodsType GoodsType { get; set; }
        public string PaymentData { get; set; }

        /// <summary>
        /// 是否提现
        /// </summary>
        public bool IsWithdrawCash { get; set; }

        public BussinessState BussinessState { get; set; }

        public Guid? PropBagId { get; set; }


        [ForeignKey("WechatMerchantId")]
        public virtual WechatMerchant WechatMerchant { get; set; }

        public int? WechatMerchantId { get; set; }

        /// <summary>
        /// 收益
        /// </summary>
        //public double Profit { get; set; }

        /// <summary>
        /// 经纪公司编号
        /// </summary>
        public int? CompanyId { get; set; }
        /// <summary>
        /// 对应的应用ID（商户应用）
        /// </summary>
        public string App_ID { get; set; }
        /// <summary>
        ///  商户ID
        /// </summary>
        public  string Seller_ID { get; set; }

    }
    public enum BussinessState
    {
        NotYet=0,
        /// <summary>
        /// 已完成
        /// </summary>
        Completed=1,
        /// <summary>
        /// 临时使用（补贴流水）
        /// </summary>
        Temporary=2,
    }
    public enum GoodsType
    {
        [EnumDescription("充值金币")]
        RechargeCoin = 1,

        [EnumDescription("转职")]
        ChangeProfession = 2,

        [EnumDescription("解锁微信号")]
        UnLockWeChatAccount = 3,

        [EnumDescription("购买大礼包")]
        BuyBigGiftBag = 4,

        [EnumDescription("解散家庭")]
        DismissFamily = 5,

        [EnumDescription("补贴流水")]
        Subsidy = 6,

    }

    public enum PaymentType
    {
        /// <summary>
        /// H5支付
        /// </summary>
        [EnumDescription("微信公众号支付")]
        WeCahtPub = 1,

        /// <summary>
        /// 小程序
        /// </summary>

        [EnumDescription("微信小程序支付")]
        MinProgram = 2,

        [EnumDescription("支付宝支付")]
        AliPay = 3

    }
}