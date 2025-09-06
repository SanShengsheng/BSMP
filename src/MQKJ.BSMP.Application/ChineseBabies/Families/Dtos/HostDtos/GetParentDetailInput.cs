using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Dtos.HostDtos
{
  public  class GetParentDetailInput
    {
        public int FamilyId { get; set; }

        public Guid PlayerGuid { get; set; }

        public string Kinship { get; set; }
    }
}
