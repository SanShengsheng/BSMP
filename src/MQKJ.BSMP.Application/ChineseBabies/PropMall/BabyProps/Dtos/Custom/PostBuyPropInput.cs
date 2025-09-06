using System;

namespace MQKJ.BSMP.ChineseBabies
{
    public class PostBuyPropInput
    {
        public int BabyId { get; set; }

        public int PropId { get; set; }

        public int FamilyId { get; set; }

        public Guid PlayerGuid { get; set; }
        /// <summary>
        /// 价格编号
        /// </summary>
        public int PriceId { get; set; }
    }
}