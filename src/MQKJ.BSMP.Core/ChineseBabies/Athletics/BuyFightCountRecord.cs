using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies.Athletics;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 购买对战次数记录表
    /// </summary>
    [Table("BuyFightCountRecords")]
    public class BuyFightCountRecord:FullAuditedEntity<Guid>
    {
        public Guid? PurchaserId { get; set; }

        /// <summary>
        /// 购买人
        /// </summary>
        public Player Purchaser { get; set; }

        public int? FamilyId { get; set; }

        public Family Family { get; set; }

        /// <summary>
        /// 对战次数
        /// </summary>
        public int FightCount { get; set; }

        /// <summary>
        /// 金币数量
        /// </summary>
        public double CoinCount { get; set; }

        /// <summary>
        /// 赛季Id
        /// </summary>
        public int SeasonManagementId { get; set; }

        public SeasonManagement SeasonManagement { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public int BabyId { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public SourceType SourceType { get; set; }
    }
}
