using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.LoveCardFiles;
using MQKJ.BSMP.LoveCardOptions;
using MQKJ.BSMP.PlayerLabels;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.LoveCards
{
    [Table("LoveCards")]
    public class LoveCard: FullAuditedEntity<Guid>
    {
        public virtual Guid PlayerId { get; set; }

        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }

        public virtual string Username { get; set; }

        ///// <summary>
        ///// 性别
        ///// </summary>
        //public virtual PlayerGender Gender { get; set; }

        /// <summary>
        /// 样式编码
        /// </summary>
        public virtual int StyleCode { get; set; } 

        /// <summary>
        /// 名片编码
        /// </summary>
        public virtual string CardCode { get; set; }

        /// <summary>
        /// 卡片状态
        /// </summary>
        public virtual int State { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public virtual int LikeCount { get; set; }

        /// <summary>
        /// 分享数
        /// </summary>
        public virtual int ShareCount  { get; set; }

        /// <summary>
        /// 保存数
        /// </summary>
        public virtual int SaveCount { get; set; }

        /// <summary>
        /// 浏览量
        /// </summary>
        public virtual int ViewsCount { get; set; }

        //public bool IsLiked { get; set; }

        public virtual LoveCardOption LoveCardOption { get; set; }

        public ICollection<LoveCardFile> LoveCardFiles { get; set; }

        public ICollection<PlayerLabel> PlayerLabels { get; set; }
    }
}
