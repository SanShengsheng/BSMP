namespace MQKJ.BSMP.AliPay
{
    public class YunZhangHuPayData
    {
        public YunZhangHuPayData()
        {

        }
        /// <summary>
        /// 商户订单号，由商户保持唯⼀一性(必填)，64个英⽂文字符以内
        /// </summary>
        public string Order_ID { get; set; }
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
        public string   Notes { get; set; }
        /// <summary>
        /// 打款备注(选填，最⼤大20个字符，⼀一个汉字占2个字符，不不允许特殊字符：' " & | @ % * ( ) - : # ￥) 
        /// </summary>
        public string Pay_Remark { get; set; }
        /// <summary>
        /// 校验⽀支付宝账户姓名，可填 Check、NoCheck 
        /// </summary>
        public string Check_Name { get; set; }
        /// <summary>
        /// 回调地址(选填，最⼤大⻓长度为200) 
        /// </summary>
        public string Notify_Url { get; set; }


    }
}
