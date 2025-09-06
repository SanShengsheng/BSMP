using System;

namespace MQKJ.BSMP.AliPay
{
    public class PostUpdateWithdrawApplyRecordInput
    {
        public string Notify_ID { get; set; }

        public DateTime Notify_Time { get; set; }

        public PostUpdateWithdrawApplyRecordInputData Data { get; set; }

    }

    public class PostUpdateWithdrawApplyRecordInputData
    {
        /// <summary>
        /// 商户订单号，由商户保持唯⼀一性(必填)，64个英⽂文字符以内
        /// </summary>
        public Guid Order_ID { get; set; }
        /// <summary>
        /// 商户代码(必填)
        /// </summary>
        public string Dealer_ID { get; set; }
        /// <summary>
        /// 代征主体(必填) 
        /// </summary>
        public string Broker_ID { get; set; }
        /// <summary>
        /// 姓名(必填) 
        /// </summary>
        public string Real_Name { get; set; }
        /// <summary>
        /// 身份证(必填)
        /// </summary>
        public string ID_Card { get; set; }
        /// <summary>
        /// 收款⼈人⽀支付宝账户(必填) 
        /// </summary>
        public string Card_NO { get; set; }
        /// <summary>
        /// 打款⾦金金额（单位为元, 必填）
        /// </summary>
        public string Pay { get; set; }
        /// <summary>
        ///  备注信息(选填) 
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// 打款备注(选填，最⼤大20个字符，⼀一个汉字占2个字符，不不允许特殊字符：' " & | @ % * ( ) - : # ￥) 
        /// </summary>
        public string Pay_Remark { get; set; }

        /// <summary>
        /// 订单状态码
        /// </summary>
        public CloudServiceOrderStatusCodeEnum Status { get; set; }
        /// <summary>
        /// 订单详细状态码
        /// </summary>
        public string Status_Detail { get; set; }
        /// <summary>
        /// 状态码说明
        /// </summary>
        public string Status_Message { get; set; }
        /// <summary>
        /// 状态详细状态码说明，详⻅见：附录2订单详细
        /// </summary>
        public string Status_Detail_Message { get; set; }
        /// <summary>
        /// 代征主体打款商户订单号
        /// </summary>
        public string Broker_Wallet_Ref { get; set; }
        /// <summary>
        /// 代征主体打款交易流水号
        /// </summary>
        public string Broker_Bank_Bill { get; set; }
    }
    /// <summary>
    /// 云账户订单状态码枚举
    /// 注：1.银⾏行行卡和⽀支付宝通道打款，仅需要订单状态码status即可判断订单是否打款成功：1成功，2或9失败，15取消⽀支付。2.对于状态1（已打款），在⽆无退汇情况下是最终状态（退汇存在于微信和银⾏行行卡通道：微信红包退回；⽤用户银⾏行行卡为II/III类银⾏行行卡收款超限额，导致打款先成功后退汇（较少情况））。
    /// </summary>
    public enum CloudServiceOrderStatusCodeEnum
    {
        /// <summary>
        /// 订单提交到⽀支付⽹网关成功（中间状态，会回调）
        /// </summary>
        Success = 1,
        /// <summary>
        /// 主要表示订单数据校验不不通过（最终状态，会回调）
        /// </summary>
        Fail = 2,
        /// <summary>
        /// 暂停处理理，满⾜足条件后会继续⽀支付，例例如账户余额不不⾜足，充值后可以继续打款（中间状态，会回调）
        /// </summary>
        Pause = 4,
        /// <summary>
        /// 调⽤用⽀支付⽹网关超时等状态异常情况导致，处于等待交易易查证的中间状态（中间状态，不不会回调）
        /// </summary>
        Unknown = 5,
        /// <summary>
        /// 订单税务筹划完毕，等待执⾏行行打款的状态（中间状态，不不会回调）
        /// </summary>
        Waiting = 8,
        /// <summary>
        /// 支付被退回（最终状态，会回调）
        /// </summary>
        FailAndRefunded = 9,
        /// <summary>
        /// 表示待打款(暂停处理理)订单数据被商户主动取消（最终状态，会回调）
        /// </summary>
        Cancle = 15,

    }
}