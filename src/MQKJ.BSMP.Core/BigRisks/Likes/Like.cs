using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.Likes
{
    [Table("Likes")]
    public class Like: FullAuditedEntity<Guid>
    {

        /// <summary>
        /// 玩家Id
        /// </summary>
        public virtual Guid PlayerId { get; set; }

        /// <summary>
        /// 问题编号
        /// </summary>
        public virtual int QuestionId { get; set; }

        /// <summary>
        /// 状态 0-取消 1-点赞 默认0
        /// </summary>
        public LikeState State { get; set; }
    }
}
