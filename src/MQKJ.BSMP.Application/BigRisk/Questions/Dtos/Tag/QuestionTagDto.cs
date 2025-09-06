using MQKJ.BSMP.TagTypes;
using System;
using System.Collections.Generic;
using System.Text;
using MQKJ.BSMP.Questions.Dtos;
using MQKJ.BSMP.Tags.Dtos;
using MQKJ.BSMP.TagTypes.Dtos;

namespace MQKJ.BSMP.Questions
{
   public class QuestionTagDto
    {
        public  int Id { get; set; }
        public  virtual QuestionTagListDto Tag { get; set; }
      
    }
}
