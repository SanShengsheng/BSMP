namespace MQKJ.BSMP.ChineseBabies
{
    public class BabyGrowUpInput
    {
        /// <summary>
        /// 宝宝编号
        /// </summary>
        public int BabyId { get; set; }
        /// <summary>
        /// 宝宝属性
        /// </summary>
        public BabyGrowUpInputBabyProperty BabyProperty { get; set; }
    }
    public class BabyGrowUpInputBabyProperty
    {
        /// <summary>
        /// 智力
        /// </summary>
        public virtual int Intelligence { get; set; }
        /// <summary>
        /// 体魄
        /// </summary>
        public virtual int Physique { get; set; }
        /// <summary>
        /// 想象
        /// </summary>
        public virtual int Imagine { get; set; }
        /// <summary>
        /// 意志
        /// </summary>
        public virtual int WillPower { get; set; }
        /// <summary>
        /// 情商
        /// </summary>
        public virtual int EmotionQuotient { get; set; }
        /// <summary>
        /// 魅力
        /// </summary>
        public virtual int Charm { get; set; }
        /// <summary>
        /// 健康
        /// </summary>
        public virtual int Healthy { get; set; }
        /// <summary>
        /// 精力
        /// </summary>
        public virtual int Energy { get; set; }
    }
}