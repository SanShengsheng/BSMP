using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos
{
    public class BuyFightCountInput
    {
        public int? FamilyId { get; set; }

        /// <summary>
        /// 购买者
        /// </summary>
        public Guid? PlayerId { get; set; }

        public int? BabyId { get; set; }

        /// <summary>
        /// 购买次数
        /// </summary>
        public int Count { get; set; }

    }
}
