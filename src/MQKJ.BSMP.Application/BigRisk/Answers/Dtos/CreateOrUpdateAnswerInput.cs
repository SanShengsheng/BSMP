using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Questions;

namespace MQKJ.BSMP.Answers.Dtos
{
    public class CreateOrUpdateAnswerInput
    {
        [Required]
        public AnswerEditDto Answer { get; set; }


		
		//// custom codes 
		
        //// custom codes end
    }
}