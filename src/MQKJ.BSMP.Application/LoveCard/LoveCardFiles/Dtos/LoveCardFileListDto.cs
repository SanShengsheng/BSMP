

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.LoveCardFiles;

namespace MQKJ.BSMP.LoveCardFiles.Dtos
{
    public class LoveCardFileListDto : FullAuditedEntityDto<Guid> 
    {

        
		/// <summary>
		/// BSMPFileId
		/// </summary>
		public Guid BSMPFileId { get; set; }



		/// <summary>
		/// UserId
		/// </summary>
		public Guid UserId { get; set; }




    }
}