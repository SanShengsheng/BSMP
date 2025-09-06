

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.SystemMessages;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.SystemMessages.Dtos
{
    public class SystemMessageListDto :  BabyPropertyBase<int>, ISearchOutModel<SystemMessage, int>
    {

        
		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; set; }



		/// <summary>
		/// NoticeType
		/// </summary>
		public NoticeType NoticeType { get; set; }



		/// <summary>
		/// PeriodType
		/// </summary>
		public PeriodType PeriodType { get; set; }



		/// <summary>
		/// PriorityLevel
		/// </summary>
		public int PriorityLevel { get; set; }



		/// <summary>
		/// ExprieDateTime
		/// </summary>
		public DateTime ExprieDateTime { get; set; }



		/// <summary>
		/// StartDateTime
		/// </summary>
		public DateTime StartDateTime { get; set; }




    }
}