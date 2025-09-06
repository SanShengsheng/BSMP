using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.Adviertisements.Dtos
{
    public class ClickAdInput
    {
        public int? AdId { get; set; }

        public Guid? PlayerId { get; set; }

        public string IpAddress { get; set; }

        public string UUID { get; set; }
    }
}
