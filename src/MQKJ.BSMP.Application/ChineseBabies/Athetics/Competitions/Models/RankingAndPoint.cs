using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Models
{
    public class RankingAndPoint
    {
        /// <summary>
        /// 当前的积分
        /// </summary>
        public int CurrentPoint { get; set; }


        /// <summary>
        /// 上次的积分
        /// </summary>
        public int LastPoint { get; set; }

        /// <summary>
        /// 当前的排名
        /// </summary>
        //public int CurrentRanking { get; set; }


        /// <summary>
        /// 上次的排名
        /// </summary>
        //public int LastRanking { get; set; }

        /// <summary>
        /// 积分变化值
        /// </summary>
        public int PointChangeValue { get; set; }

        /// <summary>
        /// 当前段位
        /// </summary>
        public DanGrading CurrentDanGrading { get; set; }

        /// <summary>
        /// 上次的段位
        /// </summary>
        public DanGrading LastDanGrading { get; set; }

        /// <summary>
        /// 对方当前的段位
        /// </summary>
        public DanGrading OtherCurrentDanGrading { get; set; }

        /// <summary>
        /// 对方上次的段位
        /// </summary>
        public DanGrading OtherLastDanGrading { get; set; }

        /// <summary>
        /// 自己的胜利次数
        /// </summary>
        public int WinCount { get; set; }

        /// <summary>
        /// 对方的胜利次数
        /// </summary>
        public int OtherWinCount { get; set; }

        /// <summary>
        /// 奖励的金币
        /// </summary>
        public int RewardCoin { get; set; }

        public PrizeRewardModel PrizeModel { get; set; }
    }
}
