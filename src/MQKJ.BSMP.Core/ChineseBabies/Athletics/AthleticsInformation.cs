using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athletics
{
    [Table("AthleticsInformations")]
    public class AthleticsInformation:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 接收方
        /// </summary>
        public Guid? ReceiverId { get; set; }

        public Player Receiver { get; set; }

        /// <summary>
        /// 发送方
        /// </summary>
        //public Guid? SenderId { get; set; }

        /// <summary>
        /// 家庭Id
        /// </summary>
        public int? FamilyId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        public AthleticsInformationType AthleticsInformationType { get; set; }

        /// <summary>
        /// 赛季Id
        /// </summary>
        public int SeasonManagementId { get; set; }
    }

    public enum AthleticsInformationType
    {
        /// <summary>
        /// 胜利次数消息
        /// </summary>
        WinCountInforamtion = 1,

        /// <summary>
        /// 对战消息
        /// </summary>
        ConfrontationInforamtion = 2,


        /// <summary>
        /// 排名消息
        /// </summary>
        RankingInforamtion = 3,

        /// <summary>
        /// 发奖消息
        /// </summary>
        PrizeInforamtion = 4,

        /// <summary>
        /// 段位消息
        /// </summary>
        DanGrading = 5
    }
}
