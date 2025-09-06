using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using MQKJ.BSMP.Answers;
using MQKJ.BSMP.Authorization.Users;
using MQKJ.BSMP.BSMPFiles;
using MQKJ.BSMP.QuestionBanks;
using MQKJ.BSMP.Scenes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MQKJ.BSMP.Questions
{
    /// <summary>
    /// 问题
    /// </summary>
    [Table("Questions")]
    public class Question : FullAuditedEntity
    {
        /// <summary>
        /// 场景编号
        /// </summary>
        [DisplayName("场景编号")]
        public virtual int SceneId { get; set; }

        /// <summary>
        /// 主场属性（男：0，女：1）
        /// </summary>
        public QuestionGender HomeField { get; set; }

        /// <summary>
        /// 主场默认图片编号
        /// </summary>
        public virtual Guid? DefaultImgId { get; set; }

        /// <summary>
        /// 背景故事（男）
        /// </summary>
        [StringLength(5000)]
        [Required]
        public virtual string BackgroundStoryMale { get; set; }

        /// <summary>
        /// 背景故事（女）
        /// </summary>
        [StringLength(5000)]
        [Required]
        public virtual string BackgroundStoryFemale { get; set; }

        /// <summary>
        /// 问题（男）
        /// </summary>
        [Required]
        [StringLength(72)]
        public virtual string QuestionMale { get; set; }

        /// <summary>
        /// 问题（女）
        /// </summary>
        [Required]
        [StringLength(72)]
        public virtual string QuestionFemale { get; set; }
        /// <summary>
        /// 追求者（男：0，女：1）
        /// </summary>
        public virtual QuestionGender Pursuer { get; set; }

        /// <summary>
        /// 状态（1-冻结）
        /// </summary>
        public virtual QuestionState State { get; set; }

        /// <summary>
        /// 回答的次数
        /// </summary>
        public virtual int AnswerCount { get; set; }

        /// <summary>
        /// 查看的次数，包括后台管理查看
        /// </summary>
        public virtual int ViewCount { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort { get; set; }

        /// <summary>
        /// 审核人编号
        /// </summary>
        public virtual long? AuditorId { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public virtual User Auditor { get; set; }

        public virtual User Creator { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public virtual DateTime? AuditDateTime { get; set; }
        /// <summary>
        /// 选项（答案）集合
        /// </summary>
        public virtual ICollection<Answer> Answers { get; set; }
        /// <summary>
        /// 校验人编号
        /// </summary>
        public virtual long? CheckOneId { get; set; }

        /// <summary>
        /// 校验
        /// </summary>
        public virtual User CheckOne { get; set; }

        /// <summary>
        /// 校验日期
        /// </summary>
        public virtual DateTime? CheckDateTime { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; } = "";
        /// <summary>
        /// 标签集合
        /// </summary>
        public virtual ICollection<QuestionTag> QuestionTags { get; set; }

        public virtual ICollection<QuestionBank> QuestionBanks { get; set; }
        /// <summary>
        /// 场景
        /// </summary>
        //[ForeignKey("SceneId")]
        public virtual Scene Scene { get; set; }

        /// <summary>
        /// 背景图片
        /// </summary>
        //[CanBeNull]

        public virtual BSMPFile DefaultImg { get; set; }

        public Question()
        {
            CreationTime = Clock.Now;
            IsDeleted = false;
        }
        public Question(int _sceneID, QuestionGender _homeField, Guid _defaultImgID, string _backgroundStoryMale, string _backgroundStoryFemale, QuestionGender _pursuer, QuestionState _state)
        {
            SceneId = _sceneID;
            HomeField = _homeField;
            DefaultImgId = _defaultImgID;
            BackgroundStoryMale = _backgroundStoryMale;
            BackgroundStoryFemale = _backgroundStoryFemale;
            Pursuer = _pursuer;
            State = _state;
        }

    }
}
