using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.BabyEvents.Dtos
{
    public class GetCoupeEventAndRechargeMessageInput
    {
        public int FamilyId { get; set; }

        public Guid PlayerId { get; set; }
    }
}
