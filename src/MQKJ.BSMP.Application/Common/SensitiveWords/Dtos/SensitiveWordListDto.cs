

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Common.SensitiveWords;

namespace MQKJ.BSMP.Common.SensitiveWords.Dtos
{
    public class SensitiveWordListDto :ISearchOutModel<SensitiveWord,int>
    {

        
		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; set; }




    }
}