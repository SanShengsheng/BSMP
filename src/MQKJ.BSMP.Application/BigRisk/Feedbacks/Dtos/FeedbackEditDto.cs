
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Feedbacks;

namespace  MQKJ.BSMP.Feedbacks.Dtos
{
    [AutoMapTo(typeof(Feedback))]
    public class FeedbackEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         



		/// <summary>
		/// PlayerId
		/// </summary>
		public Guid PlayerId { get; set; }



		/// <summary>
		/// QuestionId
		/// </summary>
		public int QuestionId { get; set; }



		/// <summary>
		/// TopicId
		/// </summary>
		public int TopicId { get; set; }



		/// <summary>
		/// Content
		/// </summary>
		//[Required(ErrorMessage="Content不能为空")]
		public string Content { get; set; }




    }
}