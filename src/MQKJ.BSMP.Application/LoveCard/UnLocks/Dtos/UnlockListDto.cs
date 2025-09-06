

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.UnLocks;

namespace MQKJ.BSMP.UnLocks.Dtos
{
    public class UnlockListDto : FullAuditedEntityDto<Guid> 
    {

        
		/// <summary>
		/// UnLockerId
		/// </summary>
		public Guid UnLockerId { get; set; }



		/// <summary>
		/// BeUnLockerId
		/// </summary>
		public Guid BeUnLockerId { get; set; }




    }
}