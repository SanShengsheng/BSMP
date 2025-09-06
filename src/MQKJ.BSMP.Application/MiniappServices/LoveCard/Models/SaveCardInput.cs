using Microsoft.AspNetCore.Http;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;

namespace MQKJ.BSMP.MiniappServices.LoveCard.Models
{
    public class SaveCardInput
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

        public PlayerGender playerGender { get; set; }

        /// <summary>
        /// 居住地
        /// </summary>
        public string Domicile { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 标签，多个用逗号隔开
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 蒙版样式id
        /// </summary>
        public int StyleCode { get; set; }

        /// <summary>
        /// 自我介绍
        /// </summary>
        public string Introduce { get; set; }

    }
}
