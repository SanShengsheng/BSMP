using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos
{
    public class EnterAtheticsInput
    {
        public int? BabyId { get; set; }

        public int? FamilyId { get; set; }


        public Guid? PlayerId { get; set; }

    }
}
