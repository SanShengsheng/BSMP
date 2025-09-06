using System;

namespace MQKJ.BSMP.LoveCard.LoveCards.Dtos
{
    public class UpdateLoveCardOtherInfoInput
    {
        public Guid PlayerId { get; set; }
        /// <summary>
        /// 自我介绍
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 卡片Id
        /// </summary>
        public Guid LoveCardId { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string Profession { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// 体重
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChatAccount { get; set; }
    }
}
