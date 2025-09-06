using MQKJ.BSMP.ChineseBabies;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Message.Dtos
{
    public class HasNewInformationRequest
    {
        public int? FamilyId { get; set; }
        public InformationType? InformationType { get; set; }
        public Guid? PlayerId { get; set; }
        public string OpenId { get; set; }

        public int? BabyId { get; set; }
    }
}
