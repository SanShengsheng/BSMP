using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Groups.Dtos
{
    public class AddEventsInput
    {
        public int GroupId { get; set; }
        public IEnumerable<int> EventIds { get; set; }
    }
}
