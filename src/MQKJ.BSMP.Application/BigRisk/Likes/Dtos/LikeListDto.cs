

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Likes;
using Abp.AutoMapper;

namespace MQKJ.BSMP.Likes.Dtos
{
    [AutoMapFrom(typeof(Like))]
    public class LikeListDto
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
		/// State
		/// </summary>
		public LikeState State { get; set; }




    }
}