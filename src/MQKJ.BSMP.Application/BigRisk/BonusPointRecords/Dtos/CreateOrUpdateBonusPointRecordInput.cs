using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.BonusPoints;

namespace MQKJ.BSMP.BonusPointRecords.Dtos
{
    public class CreateOrUpdateBonusPointRecordInput
    {
        [Required]
        public BonusPointRecordEditDto BonusPointRecord { get; set; }


		
		//// custom codes 
		
        //// custom codes end
    }
}