using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.TagTypes;
using System.Collections.Generic;
using MQKJ.BSMP.Tags;

namespace MQKJ.BSMP.TagTypes.Dtos
{ 
    public class TagTypeListDto : FullAuditedEntityDto
    {
    

/// <summary>
/// TypeName
/// </summary>
[Required(ErrorMessage="TypeName不能为空")]
public string TypeName { get; set; }


/// <summary>
/// Tags
/// </summary>
public ICollection<Tag> Tags { get; set; }


/// <summary>
/// Code
/// </summary>
public string Code { get; set; }



		
		
		
		//// custom codes 
		 
		 

        
        
        //// custom codes end
    }
}