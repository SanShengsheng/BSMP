using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.PlayerLabels.Dtos
{
    public class CreatePlayerLabelInput
    {
        public Guid LoveCardId { get; set; }

        public Guid PlayerId { get; set; }

        public ICollection<string> Labels { get; set; }
    }
}
