using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.TagTypes;

namespace MQKJ.BSMP.TagTypes.Dtos
{
    public class CreateOrUpdateTagTypeInput
    {
        [Required]
        public TagTypeEditDto TagType { get; set; }


		
		//// custom codes 
		 
		 
		
        
        
        //// custom codes end
    }
}