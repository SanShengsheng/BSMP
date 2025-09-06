using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.LoveCards;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.PlayerLabels
{
    [Table("PlayerLabels")]
    public class PlayerLabel: FullAuditedEntity<Guid>
    {
        public virtual string LabelContent { get; set; }


        //public virtual int LabelType { get; set; }

        public virtual Guid LoveCardId { get; set; }

        //public virtual LoveCard LoveCard { get; set; }
    }
}
