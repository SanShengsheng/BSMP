using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.BonusPoints;

namespace MQKJ.BSMP.BonusPoints.Dtos
{
    public class CreateOrUpdateBonusPointInput
    {
        [Required]
        public BonusPointEditDto BonusPoint { get; set; }


		
		//// custom codes 
		
        //// custom codes end
    }
}