using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.LoveCards;
using MQKJ.BSMP.PlayerDramas;
using MQKJ.BSMP.PlayerLabels;
using MQKJ.BSMP.StoryLines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.Players
{
    public class Player : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        public int TenantId { get; set; }
        /// <summary>
        /// 微信名称
        /// </summary>
        //[Required]
        public virtual string NickName { get; set; }

        /// <summary>
        /// 头像图片地址
        /// </summary>
        public virtual string HeadUrl { get; set; }

        /// <summary>
        /// 性别 1男性 2女性 0未知
        /// </summary>
        public virtual int Gender { get; set; }

        /// <summary>
        /// 年龄段 1是22以下 2是22-28 3是28以上 0未知
        /// </summary>
        public virtual int AgeRange { get; set; }


        /// <summary>
        /// 是否是代理
        /// </summary>
        public virtual bool IsAgenter { get; set; }


        /// <summary>
        /// 微信openId
        /// </summary>
        public string OpenId { get; set; }


        /// <summary>
        /// 养娃公众号的openid
        /// </summary>
        public string ChineseBabyPubOpenId { get; set; }

        /// <summary>
        /// 用户在开放平台的唯一标识符，在满足 UnionID 下发条件的情况下会返回
        /// </summary>
        public string UnionId { get; set; }

        /// <summary>
        /// 信息修改次数(性别、年龄段) 只能设置两次
        /// </summary>
        public int ModifyCount { get; set; }

         /// <summary>
        /// 授权时间
        /// </summary>
        public virtual DateTime AuthorizeDateTime { get; set; }

        /// <summary> 
        /// 玩家状态  0授权 1未授权 2冻结 
        /// </summary>
        public virtual int State { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public virtual DateTime? BirthDate { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        [Range(1,10)]
        public virtual string Profession { get; set; }

        /// <summary>
        /// 居住地
        /// </summary>
        public virtual string Domicile { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public virtual string WeChatAccount { get; set; }

        /// <summary>
        /// 设备品牌
        /// </summary>
        public string DeviceModel { get; set; }

        /// <summary>
        /// 设备系统
        /// </summary>
        public string DeviceSystem { get; set; }

        /// <summary>
        /// 是否是新手
        /// </summary>
        //public bool IsNovice { get; set; }

        /// <summary>
        /// 是否为开发者
        /// </summary>
        public bool IsDeveloper { get; set; }
        /// <summary>
        /// 是否为超级会员
        /// </summary>
        public bool IsSuperMember { get; set; }

        /// <summary>
        /// 邀请者Id
        /// </summary>
        public Guid? InviterId { get; set; }

        [ForeignKey("PlayerExtensionId")]
        public PlayerExtension PlayerExtension { get; set; }

        public int? PlayerExtensionId { get; set; }
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 游戏被邀请人
        /// </summary>
        public ICollection<GameTask> GameTaskInvitees { get; set; }
        /// <summary>
        /// 游戏邀请人
        /// </summary>
        public ICollection<GameTask> GameTaskInviters { get; set; }
        // public virtual List<BonusPoint> BonusPoints { get; set; }
        public ICollection<PlayerDrama> PlayerDramas { get; set; }

        public ICollection<StoryLine> StoryLinesPlayerA { get; set; }
        public ICollection<StoryLine> StoryLinesPlayerB { get; set; }

        public virtual ICollection <Family> FamilyFathers { get; set; }
        public virtual ICollection<Family> FamilyMothers { get; set; }

        //public ICollection<LoveCard> LoveCards { get; set; }

        public bool IsAddMask { get; set; }
    }
}
