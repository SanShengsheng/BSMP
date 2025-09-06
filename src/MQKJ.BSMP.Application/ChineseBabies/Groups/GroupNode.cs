using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Groups
{
    public class GroupNode : EventGroup
    {
        public int NextId { get; set; }
        public GroupNode Next { get; set; }
    }
}
