
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.SystemMessages;

namespace  MQKJ.BSMP.SystemMessages.Dtos
{
    public class SystemMessageEditDto : BabyPropertyBase<int>, IAddModel<SystemMessage, int>, IEditModel<SystemMessage, int>
    {

        /// <summary>
        /// Id
        /// </summary>
        public new int? Id { get; set; }         


        
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