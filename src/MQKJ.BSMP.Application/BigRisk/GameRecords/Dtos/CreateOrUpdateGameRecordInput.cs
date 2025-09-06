using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.GameRecords;

namespace MQKJ.BSMP.GameRecords.Dtos
{
    public class CreateOrUpdateGameRecordInput
    {
        [Required]
        public GameRecordEditDto GameRecord { get; set; }


		
		//// custom codes 
		
        //// custom codes end
    }
}