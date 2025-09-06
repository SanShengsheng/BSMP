using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Babies.Dtos.PrestigeDtos
{
    public class RankPrestigesItem
    {
        /// <summary>
        /// 今日是否已经膜拜
        /// </summary>
        public bool IsWorshipedToday { get; set; } = false;
        /// <summary>
        /// 当前宝宝名字
        /// </summary>
        public string BabyName { get; set; } = string.Empty;
        /// <summary>
        /// 宝宝年龄描述信息
        /// </summary>
        public string Age { get; set; }
        /// <summary>
        /// 声望值
        /// </summary>
        public int Popularity { get; set; }
        /// <summary>
        /// 今日被膜拜的次数
        /// </summary>
        public int TimesToday { get; set; }
        /// <summary>
        /// 家庭ID
        /// </summary>
        public int FamilyId { get; set; }
        /// <summary>
        /// 名次
        /// </summary>
        public int RankingNumber { get; set; } = -1;
        /// <summary>
        /// 剩余膜拜次数
        /// </summary>
        public int Surplus { get; set; } = -1;
        /// <summary>
        /// 宝宝ID
        /// </summary>
        public int ? BabyId { get; set; }

    }
}
