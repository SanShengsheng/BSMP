using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Babies.Dtos.PrestigeDtos
{
    public class GoToWorshipInput
    {
        /// <summary>
        /// 当前家庭ID
        /// </summary>
        public string BabyId { get; set; }
        /// <summary>
        /// 被膜拜的家庭ID
        /// </summary>
        public string WorshipedBabyId { get; set; }
        /// <summary>
        /// 玩家ID
        /// </summary>
        public string PlayerId { get; set; }
    }
}
