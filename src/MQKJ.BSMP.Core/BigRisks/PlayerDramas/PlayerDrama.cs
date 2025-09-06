using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Dramas;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.PlayerDramas
{
    /// <summary>
    /// 玩家剧情
    /// </summary>
   public   class PlayerDrama:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 玩家编号
        /// </summary>
        public  virtual Guid PlayerId { get; set; }

        /// <summary>
        /// 剧情编号
        /// </summary>
        public  virtual int DramId { get; set; }

        /// <summary>
        /// 是否通过考试（累计10道题目）
        /// </summary>
        public  virtual bool IsSkilled { get; set; }

        [ForeignKey("DramaId")]
        public virtual Drama Drama { get; set; }

        [ForeignKey("PlayerId")]
       public  virtual Player Player { get; set; }


    }
}
