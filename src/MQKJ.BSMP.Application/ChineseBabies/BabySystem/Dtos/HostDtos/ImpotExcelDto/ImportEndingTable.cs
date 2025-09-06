using Abp.AutoMapper;

namespace MQKJ.BSMP.ChineseBabies
{
    [AutoMapTo(typeof(BabyEnding))]
    public class ImportEndingTable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    

        public int StudyType { get; set; }


        public int StudyMax { get; set; }
        public int StudyMin { get; set; }

        public string Image { get; set; }
        /// <summary>
        /// 最高智力
        /// </summary>
        public  int MaxIntelligence { get; set; }
        /// <summary>
        /// 最高体魄
        /// </summary>
        public  int MaxPhysique { get; set; }
        /// <summary>
        /// 最高想象
        /// </summary>
        public  int MaxImagine { get; set; }
        /// <summary>
        /// 最高意志
        /// </summary>
        public  int MaxWillPower { get; set; }
        /// <summary>
        /// 最高情商
        /// </summary>
        public  int MaxEmotionQuotient { get; set; }
        /// <summary>
        /// 最高魅力
        /// </summary>
        public  int MaxCharm { get; set; }

        /// <summary>
        /// 最高智力
        /// </summary>
        public  int MinIntelligence { get; set; }
        /// <summary>
        /// 最高体魄
        /// </summary>
        public  int MinPhysique { get; set; }
        /// <summary>
        /// 最高想象
        /// </summary>
        public  int MinImagine { get; set; }
        /// <summary>
        /// 最高意志
        /// </summary>
        public  int MinWillPower { get; set; }
        /// <summary>
        /// 最高情商
        /// </summary>
        public  int MinEmotionQuotient { get; set; }
        /// <summary>
        /// 最高魅力
        /// </summary>
        public  int MinCharm { get; set; }

        public int Code { get; set; }

        public int MaxProperty { get; set; }

        public int MinProperty { get; set; }

        /// <summary>
        /// 旁白
        /// </summary>
        public string Aside { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }
    }
}