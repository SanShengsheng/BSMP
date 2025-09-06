using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.PropUseRecords;

namespace MQKJ.BSMP.PropUseRecords.Dtos
{
    public class CreateOrUpdatePropUseRecordInput
    {
        [Required]
        public PropUseRecordEditDto PropUseRecord { get; set; }


		
		//// custom codes 
		
        //// custom codes end
    }
}