using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Models
{
    public class FightResult
    {
        /// <summary>
        /// 胜利者宝宝id
        /// </summary>
        public int WinnerBabyId { get; set; }

        /// <summary>
        /// 胜率
        /// </summary>
        public double WinRate { get; set; }

        /// <summary>
        /// 道具加成
        /// </summary>
        public double PropAdditionRate { get; set; }

        /// <summary>
        /// 属性加成
        /// </summary>
        public double AttributeRate { get; set; }

        /// <summary>
        /// 产生的随机数
        /// </summary>
        public double RandomNumber { get; set; }

        /// <summary>
        /// 对战结果
        /// </summary>
        public FightResultEnum FightResultEnum { get; set; }
    }
}
