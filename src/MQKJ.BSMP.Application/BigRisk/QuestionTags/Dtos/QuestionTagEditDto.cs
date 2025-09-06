using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Questions;
using MQKJ.BSMP.Tags;

namespace MQKJ.BSMP.QuestionTags.Dtos
{
    [AutoMapTo(typeof(QuestionTag))]
    public class QuestionTagEditDto:FullAuditedEntity
    {
      

        /// <summary>
        /// 问题编号
        /// </summary>
        public int QuestionId { get; set; }


        /// <summary>
        /// 标签编号
        /// </summary>
        public int TagId { get; set; }


       






        //// custom codes 

        //// custom codes end
    }
}