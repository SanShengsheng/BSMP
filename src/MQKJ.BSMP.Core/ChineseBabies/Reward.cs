using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 奖励或消耗表
    /// </summary>
    [Table("Rewards")]
    public class Reward : BabyPropertyBase<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// 类型1-奖励2-消耗
        /// </summary>
        public RewardType Type { get; set; }

        /// <summary>
        /// 金币数量
        /// </summary>
        public int CoinCount { get; set; }
        /// <summary>
        /// 幸福
        /// </summary>
        public int Happiness { get; set; }
        /// <summary>
        /// 为了导入数据使用
        /// </summary>
        public int? Code { get; set; }
        /// <summary>
        /// 精力，为了避免基类的属性过滤，影响导入、消耗等功能
        /// </summary>
        public override int Energy { get; set; }
    }

    public enum RewardType
    {
        /// <summary>
        /// 奖励
        /// </summary>
        Reward = 1,

        /// <summary>
        /// 消耗
        /// </summary>
        Consume = 2
    }
}
