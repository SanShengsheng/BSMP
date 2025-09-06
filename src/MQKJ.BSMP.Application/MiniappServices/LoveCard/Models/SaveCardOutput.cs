using System;

namespace MQKJ.BSMP.MiniappServices.LoveCard.Models
{
    public class SaveCardOutput
    {
        public string FilePath { get; set; }

        public Guid LoveCardId { get; set; }

        public string CardCode { get; set; }
    }
}
