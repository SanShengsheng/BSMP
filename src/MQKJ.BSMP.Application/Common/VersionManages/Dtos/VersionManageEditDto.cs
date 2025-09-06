
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;

namespace  MQKJ.BSMP.Common.Dtos
{
    public class VersionManageEditDto:IAddModel<VersionManage, int>,IEditModel<VersionManage, int>
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
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
		public string ReleaseLog { get; set; }



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