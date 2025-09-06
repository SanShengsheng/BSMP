

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Common.OperationActivities;
using Abp.Domain.Entities;

namespace MQKJ.BSMP.Common.OperationActivities.Dtos
{
    public class OperationActivityListDto : FullAuditedEntityDto,ISearchOutModel<OperationActivity,int>,IMustHaveTenant 
    {

        
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