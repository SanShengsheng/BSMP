using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.GameTasks;

namespace MQKJ.BSMP.GameTasks.Dtos
{
    public class CreateOrUpdateGameTaskInput
    {
        [Required]
        public GameTaskEditDto GameTask { get; set; }


		
		//// custom codes 
		
        //// custom codes end
    }
}