using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class GetBonusPointsCountOutput
    {
        /// <summary>
        /// 积分数量
        /// </summary>
        public int BonusPointCount { get; set; }

        /// <summary>
        /// 选项占比数组
        /// </summary>
        public double[] Percents { get; set; }

        public string ErrMessage { get; set; }
    }
}
