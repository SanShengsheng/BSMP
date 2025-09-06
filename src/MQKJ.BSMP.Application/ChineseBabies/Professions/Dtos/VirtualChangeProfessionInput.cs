using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Professions.Dtos
{
    public class VirtualChangeProfessionInput
    {
        public int FamilyId { get; set; }

        public Guid PlayerId { get; set; }

        public int ProfessionId { get; set; }
    }
}
