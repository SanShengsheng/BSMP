using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.BabySystem.Dtos.HostDtos
{
    public class GetMiniProgramsOutput
    {
        public int Id { get; set; }

        public string AppId { get; set; }
        public string Name { get; set; }

        public string Icon { get; set; }

        public string Description { get; set; }
        public string SubDescription { get; set; }
    }
}
