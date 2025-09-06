using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Likes.Dtos
{
    public class GetLikeStateInput
    {
        public int QuestionId { get; set; }

        public Guid PlayerId { get; set; }
    }
}
