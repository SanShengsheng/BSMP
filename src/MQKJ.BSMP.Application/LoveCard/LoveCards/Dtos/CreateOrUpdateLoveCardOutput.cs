using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.LoveCards.Dtos
{
    public class CreateOrUpdateLoveCardOutput
    {
        public Guid LoveCardId { get; set; }

        public string CardCode { get; set; }
    }
}
