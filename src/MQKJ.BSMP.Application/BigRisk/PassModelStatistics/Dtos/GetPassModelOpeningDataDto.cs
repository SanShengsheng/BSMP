using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.PassModelStatistics.Dtos
{
    public class GetPassModelOpeningDataDto
    {
        public OpeningType OpeningType { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
