namespace MQKJ.BSMP.Utils.WechatPay.Dtos
{
    public class MiniProgramPayOutput
    {
        public string AppId { get; set; }

        public string TimeStamp { get; set; }

        public string NonceStr { get; set; }

        public string Package { get; set; }

        public string SignType { get; set; }
        public string PaySign { get; internal set; }
        public string TradeNo { get; set; }
        public ChangeResult ChangeResult { get; set; }

        public string MwebUrl { get; set; }
        public string FormTableString { get; set; }
    }
}