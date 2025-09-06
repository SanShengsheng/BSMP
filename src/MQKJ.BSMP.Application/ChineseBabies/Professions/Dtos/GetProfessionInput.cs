using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Professions.Dtos
{
    public class GetProfessionInput
    {
        public Guid PlayerId { get; set; }

        public int FamilyId { get; set; }
    }
}
