using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Dtos
{
    public class GetFamilyInfoInput
    {
        public int FamilyId { get; set; }

        public Guid? PlayerGuid { get; set; }
    }
}
