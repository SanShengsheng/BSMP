

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Feedbacks;

namespace MQKJ.BSMP.Feedbacks.Dtos
{
    public class CreateOrUpdateFeedbackInput
    {
        [Required]
        public FeedbackEditDto Feedback { get; set; }

    }
}