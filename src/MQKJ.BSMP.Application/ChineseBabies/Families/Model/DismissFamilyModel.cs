using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Model
{
    public class DismissFamilyModel
    {
        public int FamilyId { get; set; }

        public Guid InitiatorId { get; set; }

        public DismissFamilyType DismissFamilyType { get; set; }

        public Guid? OrderId { get; set; }

        public FamilyState FamilyState { get; set; }

        public DateTime ExpireTime { get; set; }
    }
}
