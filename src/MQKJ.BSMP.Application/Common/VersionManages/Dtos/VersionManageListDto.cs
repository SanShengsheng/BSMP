

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.Common.Dtos
{
    public class VersionManageListDto : FullAuditedEntityDto, ISearchOutModel<VersionManage,int>
    {

        
		/// <summary>
		/// IsPopup
		/// </summary>
		public bool IsPopup { get; set; }



		/// <summary>
		/// IsForceUpdate
		/// </summary>
		public bool IsForceUpdate { get; set; }



		/// <summary>
		/// RelaseLog
		/// </summary>
		public string RelaseLog { get; set; }



		/// <summary>
		/// Version
		/// </summary>
		public string Version { get; set; }



		/// <summary>
		/// Remark
		/// </summary>
		public string Remark { get; set; }




    }
}