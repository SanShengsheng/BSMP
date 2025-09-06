
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.LoveCardFiles;
using MQKJ.BSMP.LoveCardOptions;
using MQKJ.BSMP.LoveCards;
using MQKJ.BSMP.Players;

namespace  MQKJ.BSMP.LoveCards.Dtos
{
    public class LoveCardEditDto
    {

        public Guid? Id { get; set; }

        /// <summary>
        /// 玩家Id
        /// </summary>
        public Guid PlayerId { get; set; }

        public PlayerGender Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string Profession { get; set; }

        /// <summary>
        /// 居住地
        /// </summary>
        public string Domicile { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChatAccount { get; set; }

        public string Label { get; set; }

        public int StyleCode { get; set; }




    }
}