using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.PassModelStatistics.Dtos
{
    public class GetLevelDistributionOutput
    {
        public int Floor { get; set; }

        /// <summary>
        /// 该关卡中的量
        /// </summary>
        public int FloorCount { get; set; }

        /// <summary>
        /// 平均过关失败次数
        /// </summary>
        public int AveragePassFailCount { get; set; }

        /// <summary>
        /// 最高失败次数
        /// </summary>

        public int HighestPassFailCount { get; set; }
    }
}
