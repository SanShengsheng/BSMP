using System;

namespace MQKJ.BSMP.ChineseBabies
{
    public class FixBabyPropertyErrorInput
    {
        /// <summary>
        /// 宝宝编号，更新指定宝宝
        /// </summary>
        public int? BabyId { get; set; }
        /// <summary>
        /// 宝宝最后一次更新开始时间
        /// </summary>
        public DateTime? StartDateTime { get; set; }
        /// <summary>
        /// 宝宝最后一次更新结束时间
        /// </summary>
        public DateTime? EndDateTime { get; set; }


    }
}