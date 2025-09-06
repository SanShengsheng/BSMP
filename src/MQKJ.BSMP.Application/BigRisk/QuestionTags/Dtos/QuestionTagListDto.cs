using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Questions;
using MQKJ.BSMP.Tags;

namespace MQKJ.BSMP.QuestionTags.Dtos
{
    public class QuestionTagListDto : FullAuditedEntityDto
    {

     


        /// <summary>
        /// 问题编号
        /// </summary>
        public int QuestionId { get; set; }


        /// <summary>
        /// 标签编号
        /// </summary>
        public int TagId { get; set; }


        /// <summary>
        /// 问题
        /// </summary>
        public Question Question { get; set; }


        /// <summary>
        /// 标签
        /// </summary>
        public Tag Tag { get; set; }






        //// custom codes 

        //// custom codes end
    }
}