using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Dtos
{
    public class ConfirmDismissFamilyInput
    {
        public Guid PlayerGuid { get; set; }
        public int FamilyId { get; set; }
    }
}
