using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Prestiges
{
    public class PrestigesRanksModel
    {
        /// <summary>
        /// 家庭ID
        /// </summary>
        public int FamilyId { get; set; }
        /// <summary>
        /// 声望值
        /// </summary>
        public int Popularity { get; set; }
        public int BabyId { get; set; }
        /// <summary>
        /// 宝宝名字
        /// </summary>
        public string BabyName { get; set; }
        public int Rank { get; set; }
        public string Age { get; set; }
    }
}
