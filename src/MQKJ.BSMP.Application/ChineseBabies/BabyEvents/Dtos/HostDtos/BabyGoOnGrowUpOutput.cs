using Abp.AutoMapper;

namespace MQKJ.BSMP.ChineseBabies
{
    public class BabyGoOnGrowUpOutput
    {
        public BabyGoOnGrowUpBabyStory StroyEnding { get; set; }
        public int? NextGroupId { get; set; }
    }
    [AutoMapFrom(typeof(BabyEnding))]
    public class BabyGoOnGrowUpBabyStory
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ImagePath
        {
            get => Image;
        }
        /// <summary>
        /// 旁白(描述2)
        /// </summary>
        public string Aside { get; set; }

    }
}