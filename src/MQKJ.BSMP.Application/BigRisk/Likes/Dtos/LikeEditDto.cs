
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Likes;

namespace  MQKJ.BSMP.Likes.Dtos
{
    [AutoMapTo(typeof(Like))]
    public class LikeEditDto
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