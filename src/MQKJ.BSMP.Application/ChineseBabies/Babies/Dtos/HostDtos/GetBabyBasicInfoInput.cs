using System;

namespace MQKJ.BSMP.ChineseBabies
{
    public class GetBabyBasicInfoInput
    {
        /// <summary>
        /// 宝宝编号
        /// </summary>
        public int BabyId { get; set; }

        public Guid? PlayerGuid { get; set; }
    }
}