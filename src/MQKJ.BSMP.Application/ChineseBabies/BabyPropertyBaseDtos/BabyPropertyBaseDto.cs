using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.BabyPropertyBaseDtos
{
    public class BabyPropertyBaseDto<TKey>
    {
        /// <summary>
        /// 智力
        /// </summary>
        public int Intelligence { get; set; }
        /// <summary>
        /// 体魄
        /// </summary>
        public int Physique { get; set; }
        /// <summary>
        /// 想象
        /// </summary>
        public int Imagine { get; set; }
        /// <summary>
        /// 意志
        /// </summary>
        public int WillPower { get; set; }
        /// <summary>
        /// 情商
        /// </summary>
        public int EmotionQuotient { get; set; }
        /// <summary>
        /// 魅力
        /// </summary>
        public int Charm { get; set; }
        /// <summary>
        /// 健康
        /// </summary>
        public int Healthy { get; set; }
        /// <summary>
        /// 精力
        /// </summary>
        public int Energy { get; set; }
    }
}
