using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.BuyFightCountRecords.Dtos
{
    public class BuyFightCountRecordInput
    {
        /// <summary>
        /// 购买者
        /// </summary>
        public Guid PurchaserId { get; set; }

        /// <summary>
        /// 宝宝Id
        /// </summary>
        public int BabyId { get; set; }

        /// <summary>
        /// 购买对战次数
        /// </summary>
        public int FightCount { get; set; }


        /// <summary>
        /// 来源
        /// </summary>
        public SourceType SourceType { get; set; }
    }
}
