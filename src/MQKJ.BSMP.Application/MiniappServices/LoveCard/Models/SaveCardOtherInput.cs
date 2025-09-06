using Microsoft.AspNetCore.Http;
using System;

namespace MQKJ.BSMP.MiniappServices.LoveCard.Models
{
    public class SaveCardOtherInput
    {
        /// <summary>
        /// 卡片Id
        /// </summary>
        public Guid? LoveCardId { get; set; }

        /// <summary>
        /// 玩家Id
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        //[JsonIgnore]
        public IFormFile FormFile { get; set; }
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
        /// 自我介绍
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChatAccount { get; set; }
    }
}
