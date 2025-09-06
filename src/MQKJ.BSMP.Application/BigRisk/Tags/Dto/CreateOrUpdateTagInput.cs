using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Tags;
using MQKJ.BSMP.Tags.Dtos;

namespace MQKJ.BSMP.Tags.Dto
{
    public class CreateOrUpdateTagInput
    {
        [Required]
        public TagEditDto Tag { get; set; }


		
		//// custom codes 
		
        //// custom codes end
    }
}