namespace MQKJ.BSMP.ChineseBabies.BabySystem.Dtos
{
    public class BabySystemSetting
    {
        public SystemSettingPayment Payment { get; set; }


        public class SystemSettingPayment : SystemSettingPaymentBasic
        {
            public GoldCoin GoldCoin { get; set; }
            public Energy Energy { get; set; }

        }

        public class SystemSettingPaymentBasic
        {
            public bool IsShow { get; set; }
            public bool IsEnable { get; set; }
            public string Title { get; set; }
            public string Message { get; set; }
        }
        public class GoldCoin : SystemSettingPaymentBasic
        {
        }
        public class Energy : SystemSettingPaymentBasic
        {
        }
    }
}
