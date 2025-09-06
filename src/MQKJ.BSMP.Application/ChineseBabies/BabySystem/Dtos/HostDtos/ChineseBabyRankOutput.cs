using System.Collections.Generic;

namespace MQKJ.BSMP.ChineseBabies
{
    public class ChineseBabyRankOutput
    {
        public List<ChineseBabyRankOutputDto> BabyRankingList { get; set; }

        public List<ChineseBabyRankOutputFamilyRankDto> FamilyRankingList { get; set; }

        public ChineseBabyRankOutputRankInfo RankInfo { get; set; }


    }
    public class ChineseBabyRankOutputRankInfo
    {
        public string BabyAgeRangeRankInfo { get; set; }
           
        public string HomeLevelRankInfo { get; set; }
    }
    public class ChineseBabyRankOutputDto
    {
        /// <summary>
        /// 宝宝名字
        /// </summary>
        public string BabyName { get; set; }
        ///// <summary>
        ///// 家庭收入
        ///// </summary>
        //public double Deposit { get; set; }
        /// <summary>
        /// 成长潜力
        ///成长潜力=主事件个数（未完成）+特殊事件个数（未完成）+ 可学习的次数（未学习的项）。（当前及后期的事件组）
        /// </summary>
        public double Potential { get; set; }

        public int Healthy { get; set; }
        
        public string HealthyTitle
        {
            get;set;
        }
        /// <summary>
        /// 成长总值
        /// </summary>
        public double GrowthTotal { get; set; }
        /// <summary>
        /// 宝宝排名
        /// </summary>
        public int BabyRank { get; set; }
    }

    public class ChineseBabyRankOutputFamilyRankDto
    {
        /// <summary>
        /// 宝宝排名
        /// </summary>
        public int BabyRank { get; set; }
        /// <summary>
        /// 宝宝名字
        /// </summary>
        public string BabyName { get; set; }
        /// <summary>
        /// 家庭收入
        /// </summary>
        public double Deposit { get; set; }
        /// <summary>
        /// 
        /// 家庭幸福度：幸福恩爱（100）      夫唱妇随 （75）     相敬如宾（50）      貌合神离（25）     同床异梦 （0）
        /// </summary>
        public double Happiness { get; set; }
        public string HappinessTitle { get; set; }
       
        
    }
}