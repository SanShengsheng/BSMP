using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Dtos
{
    public class CancelDismissFamilyInput
    {
        public int FamilyId { get; set; }
        public Guid PlayerGuid { get; set; }

        //public FamilyState FamilyState { get; set; }
    }
}
