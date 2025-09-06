using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.PlayerProfessions.Dtos
{
    public class GetChangedProfessionsInput
    {
        public int FamilyId { get; set; }

        public Guid PlayerId { get; set; }
    }
}
