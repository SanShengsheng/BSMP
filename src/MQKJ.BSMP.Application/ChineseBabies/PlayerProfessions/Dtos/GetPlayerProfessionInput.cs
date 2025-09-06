using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.PlayerProfessions.Dtos
{
    public class GetPlayerProfessionInput
    {
        public Guid PlayerId { get; set; }

        public int FamilyId { get; set; }
    }
}
