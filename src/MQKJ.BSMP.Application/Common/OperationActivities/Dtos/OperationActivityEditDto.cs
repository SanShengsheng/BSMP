
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Common.OperationActivities;

namespace  MQKJ.BSMP.Common.OperationActivities.Dtos
{
    public class OperationActivityEditDto:IAddModel<OperationActivity,int>,IEditModel<OperationActivity,int>
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
		/// <summary>
		/// TenantId
		/// </summary>
		public int TenantId { get; set; }



		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; set; }



		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; set; }



		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; set; }



		/// <summary>
		/// SubTitle
		/// </summary>
		public string SubTitle { get; set; }



		/// <summary>
		/// IsPopup
		/// </summary>
		public bool IsPopup { get; set; }



		/// <summary>
		/// ExpireDateTime
		/// </summary>
		public DateTime? ExpireDateTime { get; set; }



		/// <summary>
		/// StartDateTime
		/// </summary>
		public DateTime StartDateTime { get; set; }



		/// <summary>
		/// Img
		/// </summary>
		public string Img { get; set; }



		/// <summary>
		/// ActivityType
		/// </summary>
		public ActivityType ActivityType { get; set; }




    }
}