using Abp.AutoMapper;

namespace MQKJ.BSMP.ChineseBabies
{
    public class GetAudltBabyInfoOutput
    {
        public GetAudltBabyInfoOutputBabyStory StroyEnding { get; set; }
    }
    [AutoMapFrom(typeof(BabyEnding))]
    public class GetAudltBabyInfoOutputBabyStory
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath {
            get => Image;
        }
        public string Image { get; set; }
        /// <summary>
        /// 旁白(描述2)
        /// </summary>
        public string Aside { get; set; }
    }
}