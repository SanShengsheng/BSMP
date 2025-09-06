using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.Common
{
    /// <summary>
    /// 提现记录
    /// </summary>
    [Table("EnterpirsePaymentRecords")]
    public class EnterpirsePaymentRecord : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 提现金额
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 代理人
        /// </summary>
        public int AgentId { get; set; }

        public MqAgent MqAgent { get; set; }

        public WithdrawDepositState State { get; set; }

        /// <summary>
        /// 微信返回的数据
        /// </summary>
        public string PaymentData { get; set; }

        /// <summary>
        /// 微信返回的订单号
        /// </summary>
        public string PaymentNo { get; set; }

        /// <summary>
        /// 企业成功付款时间
        /// </summary>
        public DateTime PaymentTime { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        public string RequestData { get; set; }
        /// <summary>
        /// 支付通道类型
        /// </summary>
        public WithdrawMoneyType WithdrawMoneyType { get; set; }

        /// <summary>
        /// 申请支付平台，用户提现申请请求使用哪一个平台
        /// </summary>
        public WithdrawMoneyType RequestPlatform { get; set; }

        
    }

    public enum WithdrawDepositState
    {
        /// <summary>
        /// 所有
        /// </summary>
        All = -1,

        /// <summary>
        /// 初始化
        /// </summary>
        Initialization = 0,

        /// <summary>
        /// 提现成功
        /// </summary>
        [EnumHelper.EnumDescription("提现成功")]
        Success = 1,

        /// <summary>
        /// 提现失败
        /// </summary>
        [EnumHelper.EnumDescription("提现失败")]
        Fail = 2,
        /// <summary>
        /// 第三方提现失败
        /// </summary>
        [EnumHelper.EnumDescription("第三方提现失败")]
        ThirdPartyFail = 6,
        /// <summary>
        /// 发起提现审核
        /// </summary>
        [EnumHelper.EnumDescription("审核")]
        Auditing = 3,

        /// <summary>
        /// 拒绝提现
        /// </summary>
        [EnumHelper.EnumDescription("拒绝")]
        Refuse = 4,
        /// <summary>
        /// 挂单（第三方云账户）
        /// </summary>
        [EnumHelper.EnumDescription("挂单")]
        HangOrder = 5,
    }

    public enum WithdrawMoneyType
    {
        All = 0,

        /// <summary>
        /// 微信企业提现
        /// </summary>
        [EnumHelper.EnumDescription("微信企业提现")]
        WeChatEnterPayType = 1,

        /// <summary>
        /// 手动提现
        /// </summary>
        [EnumHelper.EnumDescription("人工提现")]
        ManualWithDrawType = 2,
        /// <summary>
        /// 第三方支付宝
        /// </summary>
        [EnumHelper.EnumDescription("第三方支付宝")]
        ThirdPartyPaymentALi = 3,

        /// <summary>
        /// 第三方银联（银行卡）
        /// </summary>
        [EnumHelper.EnumDescription("第三方银联")]
        ThirdPartyPaymentChinaPay = 4,
    }
}
