using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.BSMPFiles;
using MQKJ.BSMP.LoveCards;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.LoveCardFiles
{
    [Table("LoveCardFiles")]
    public class LoveCardFile : FullAuditedEntity<Guid>
    {
        public virtual Guid BSMPFileId { get; set; }

        public virtual Guid LoveCardId { get; set; }

        [ForeignKey("LoveCardId")]
        public virtual LoveCard LoveCard { get; set; }

        [ForeignKey("BSMPFileId")]
        public virtual BSMPFile BSMPFile { get; set; }
    }
}
