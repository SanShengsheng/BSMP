using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.LoveCards;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.LoveCardOptions
{
    [Table("LoveCardOptions")]
    public class LoveCardOption : FullAuditedEntity<Guid>
    {

        //[ForeignKey("LoveCardId")]
        //public virtual LoveCard LoveCard { get; set; }

        public virtual Guid LoveCardId { get; set; }

        public bool IsLiked { get; set; }

        /// <summary>
        /// 点赞或分享的人
        /// </summary>
        public virtual Player OptionPlayer { get; set; }

        public virtual Guid OptionPlayerId { get; set; }

        public virtual LoveCardOptionType LoveCardOptionType { get; set; }
    }
}
