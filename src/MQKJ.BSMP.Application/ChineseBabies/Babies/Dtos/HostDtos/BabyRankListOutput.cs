using System.Collections.Generic;

namespace MQKJ.BSMP.ChineseBabies
{
    public class BabyRankListOutput
    {
        public List<BabyRankDto> BabyRankDtos { get; set; }
    }

    public class BabyRankDto
    {
 /// <summary>
        /// 宝宝名字
        /// </summary>
        public string BabyName { get; set; }
        /// <summary>
        /// 家庭收入
        /// </summary>
        public double Deposit { get; set; }
        /// <summary>
        /// 成长潜力
        ///成长潜力=主事件个数（未完成）+特殊事件个数（未完成）+ 可学习的次数（未学习的项）。（当前及后期的事件组）
        /// </summary>
        public double Potential { get; set; }
        /// <summary>
        /// 成长总值
        /// </summary>
        public double GrowthTotal { get; set; }
        /// <summary>
        /// 宝宝排名
        /// </summary>
        public int BabyRank { get; set; }

        /// <summary>
        /// 六维属性总值
        /// </summary>
        public int SixPropertyTotalValue { get; set; }
    }
}