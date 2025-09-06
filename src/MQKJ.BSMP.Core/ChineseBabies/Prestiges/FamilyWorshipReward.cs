using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace MQKJ.BSMP.ChineseBabies.Prestiges
{
    [Table("FamilyWorshipRewards")]
    public class FamilyWorshipReward:Entity<int>,IHasCreationTime , ISoftDelete 
    {
        /// <summary>
        /// 等级下限
        /// </summary>
        public int RankMin { get; set; }
        /// <summary>
        /// 等级上限
        /// </summary>
        public int RankMax { get; set; }
        /// <summary>
        /// 金币下限
        /// </summary>
        public int CoinsMin { get; set; }
        /// <summary>
        /// 金币上限
        /// </summary>
        public int CoinsMax { get; set; }
        /// <summary>
        /// 获得声望值
        /// </summary>
        public int Prestiges { get; set; }
        /// <summary>
        /// 奖励类型
        /// </summary>
        public FamilyWorshipRewardType Type { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreationTime { get; set; } = System.DateTime.Now;
    }
}
