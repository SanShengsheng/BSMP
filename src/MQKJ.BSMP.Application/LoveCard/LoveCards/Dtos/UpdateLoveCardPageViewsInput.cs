using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.LoveCards.Dtos
{
    public class UpdateLoveCardPageViewsInput
    {
        public Guid PlayerId { get; set; }

        public Guid LoveCardId { get; set; }
    }
}
