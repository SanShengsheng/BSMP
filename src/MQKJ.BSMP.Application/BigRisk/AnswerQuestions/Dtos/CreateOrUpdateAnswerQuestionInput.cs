using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.GameTasks;

namespace MQKJ.BSMP.AnswerQuestions.Dtos
{
    public class CreateOrUpdateAnswerQuestionInput
    {
        [Required]
        public AnswerQuestionEditDto AnswerQuestion { get; set; }


		
		//// custom codes 
		 
		
        
        //// custom codes end
    }
}