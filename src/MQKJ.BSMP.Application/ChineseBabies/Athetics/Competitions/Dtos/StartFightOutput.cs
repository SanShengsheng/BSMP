using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Models;
using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos
{
    public class StartFightOutput
    {

        public StartFightErrCode StartFightErrCode { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        public double WiningRatio { get; set; }

        /// <summary>
        /// 胜利方宝宝ID
        /// </summary>
        //public int BabyId { get; set; }

        /// <summary>
        /// 属性编码
        /// </summary>
        public BabyAttributeCode BabyAttributeCode { get; set; }

        /// <summary>
        /// 对战结果
        /// </summary>
        public FightResultEnum FightResultEnum { get; set; }

        public int GamePoint { get; set; }

        /// <summary>
        /// 道具加成胜率
        /// </summary>
        public string PropAdditionRate { get; set; }


        /// <summary>
        /// 属性加成
        /// </summary>
        public string AttributeRate { get; set; }

        /// <summary>
        /// 段位
        /// </summary>
        public DanGrading DanGrading { get; set; }

        /// <summary>
        /// 奖励的金币
        /// </summary>
        public int RewardCoin { get; set; }

        public PrizeRewardModel PrizeReward { get; set; }
    }

    public enum StartFightErrCode
    {
        /// <summary>
        /// 竞技场未开启
        /// </summary>
        UnOpen = 1,

        /// <summary>
        /// 对战次数不够
        /// </summary>
        FightCountNotEnough = 2
    }
}
