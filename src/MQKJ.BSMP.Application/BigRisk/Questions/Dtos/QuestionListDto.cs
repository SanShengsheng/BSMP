using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Questions;
using System.Collections.Generic;
using MQKJ.BSMP.Answers.Dtos;
using MQKJ.BSMP.Authorization.Users;
using MQKJ.BSMP.QuestionTags.Dtos;
using MQKJ.BSMP.Tags;
using MQKJ.BSMP.Tags.Dto;
using MQKJ.BSMP.Scenes;
namespace MQKJ.BSMP.Questions.Dtos
{
    public class QuestionListDto : FullAuditedEntityDto
    {

     

        /// <summary>
        /// 场景编号
        /// </summary>
        [Required(ErrorMessage = "场景编号不能为空")]
        public int SceneId { get; set; }


        ///// <summary>
        ///// 主场
        ///// </summary>
        //[Required(ErrorMessage = "主场不能为空")]
        //public QuestionGender HomeField { get; set; }


        /// <summary>
        /// 默认图片编号
        /// </summary>
        [Required(ErrorMessage = "默认图片编号不能为空")]
        public Guid DefaultImgId { get; set; }


        /// <summary>
        /// 背景故事（男）
        /// </summary>
       // [MaxLength(160, ErrorMessage = "背景故事（男）大于于最小长度")]
        [Required(ErrorMessage = "背景故事（男）不能为空")]
        public string BackgroundStoryMale { get; set; }


        /// <summary>
        /// 背景故事（女）
        /// </summary>
       // [MaxLength(160, ErrorMessage = "背景故事（女）大于最小长度")]
        [Required(ErrorMessage = "背景故事（女）不能为空")]
        public string BackgroundStoryFemale { get; set; }

        /// <summary>
        /// 问题（男）
        /// </summary>
        [MaxLength(72, ErrorMessage = "问题（男）大于最小长度")]
        [Required(ErrorMessage = "问题（男）不能为空")]
        public virtual string QuestionMale { get; set; }

        /// <summary>
        /// 问题（女）
        /// </summary>
        [MaxLength(72, ErrorMessage = "问题（女）大于最小长度")]
        [Required(ErrorMessage = "问题（女）不能为空")]
        public virtual string QuestionFemale { get; set; }
        /// <summary>
        /// 男/女主场
        /// </summary>
        [Required(ErrorMessage = "主场不能为空")]
        public QuestionGender Pursuer { get; set; }


        /// <summary>
        /// 状态
        /// </summary>
        public QuestionState State { get; set; }


        /// <summary>
        /// 回答次数
        /// </summary>
        public int AnswerCount { get; set; }


        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ViewCount { get; set; }


        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        public  Scene Scene { get; set; }

        /// <summary>
        /// 选项
        /// </summary>
        public ICollection<AnswerListDto> Answers { get; set; }

        ///// <summary>
        ///// 问题标签
        ///// </summary>
        public ICollection<QuestionTagDto> QuestionTags { get; set; }



        //// custom codes 
        /// <summary>
        /// 审核人编号
        /// </summary>
        public virtual long? AuditorId { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public virtual User Auditor { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public virtual DateTime AuditDateTime { get; set; }

        public  virtual  User Creator { get; set; }
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
        //// custom codes end
    }
}