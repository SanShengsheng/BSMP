using System.Collections.Generic;

namespace MQKJ.BSMP.ChineseBabies
{
    public class FixBabyPropertyErrorOutput
    {
        public FixBabyPropertyErrorOutput()
        {
            BabyIds = new List<int>(); 
        }
        public List<int> BabyIds { get; set; }
    }
}