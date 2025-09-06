

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Feedbacks;

namespace MQKJ.BSMP.Feedbacks.Dtos
{
    public class FeedbackListDto : FullAuditedEntityDto<Guid> 
    {

        



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
		[Required(ErrorMessage="Content不能为空")]
		public string Content { get; set; }




    }
}