using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Babies.Dtos
{
   public class BabyBasePropertyDto
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
    }
}
