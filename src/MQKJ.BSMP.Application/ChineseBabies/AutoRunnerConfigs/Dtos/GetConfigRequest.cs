using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.AutoRunnerConfigs.Dtos
{
    public class GetConfigRequest
    {
        public Guid PlayerId { get; set; }
        public int FamilyId { get; set; }
    }
}
