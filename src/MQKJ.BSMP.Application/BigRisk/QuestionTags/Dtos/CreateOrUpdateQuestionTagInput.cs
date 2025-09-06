using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Questions;

namespace MQKJ.BSMP.QuestionTags.Dtos
{
    public class CreateOrUpdateQuestionTagInput
    {
        [Required]
        public QuestionTagEditDto QuestionTag { get; set; }


		
		//// custom codes 
		
        //// custom codes end
    }
}