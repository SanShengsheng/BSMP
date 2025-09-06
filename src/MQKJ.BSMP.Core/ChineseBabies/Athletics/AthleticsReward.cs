using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athletics
{
    /// <summary>
    /// 竞技奖励表
    /// </summary>
    [Table("AthleticsRewards")]
    public class AthleticsReward : FullAuditedEntity
    {

        ///// <summary>
        ///// 触发奖励的最大排名
        ///// </summary>
        //public int MaxRanking { get; set; }


        ///// <summary>
        ///// 触发奖励的最小排名
        ///// </summary>
        //public int MinRanking { get; set; }

        /// <summary>
        /// 排名
        /// </summary>
        public int RankingNumber { get; set; }

        /// <summary>
        /// 奖励数量
        /// </summary>
        public int BabyPropCount { get; set; }

        /// <summary>
        /// 道具Id
        /// </summary>
        public int? BabyPropId { get; set; }

        /// <summary>
        /// 道具
        /// </summary>
        public BabyProp BabyProp { get; set; }

        /// <summary>
        /// 金币数量
        /// </summary>
        public int CoinCount { get; set; }

        ///// <summary>
        ///// 奖励类型
        ///// </summary>
        //public RewardPropType RewardPropType { get; set; }

        /// <summary>
        /// 道具价格Id
        /// </summary>
        public int? BabyPropPriceId { get; set; }

        /// <summary>
        /// 道具价格
        /// </summary>
        public BabyPropPrice BabyPropPrice { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public RewardPropType RewardPropType { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public int Code { get; set; }
    }

    public enum RewardPropType
    {
        /// <summary>
        /// 金币
        /// </summary>
        [EnumHelper.EnumDescription("金币")]
        CoinType = 1,


        /// <summary>
        /// 商品
        /// </summary>
        [EnumHelper.EnumDescription("商品")]
        Prodcut = 2
    }
    //public enum RewardPropType
    //{
    //    /// <summary>
    //    /// 金币
    //    /// </summary>
    //    [EnumHelper.EnumDescription("金币")]
    //    CoinType = 1,


    //    /// <summary>
    //    /// 商品
    //    /// </summary>
    //    [EnumHelper.EnumDescription("商品")]
    //    Prodcut = 2
    //}
}
