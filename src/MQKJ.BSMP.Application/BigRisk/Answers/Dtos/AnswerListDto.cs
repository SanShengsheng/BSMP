using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Questions;

namespace MQKJ.BSMP.Answers.Dtos
{
    public class AnswerListDto : FullAuditedEntityDto
    {

      


        /// <summary>
        /// 问题编号
        /// </summary>
        [Required(ErrorMessage = "问题编号不能为空")]
        public int QuestionID { get; set; }


        /// <summary>
        /// 问题类型
        /// </summary>
        [Required(ErrorMessage = "问题类型不能为空")]
        public QuestionGender QuestionType { get; set; }


        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(72, ErrorMessage = "标题超出最大长度")]
        [Required(ErrorMessage = "标题不能为空")]
        public string Title { get; set; }


        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }









        //// custom codes 

        //// custom codes end
    }
}