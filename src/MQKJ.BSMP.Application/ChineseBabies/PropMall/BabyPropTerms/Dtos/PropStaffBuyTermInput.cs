using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.PropMall.Props.Props
{
  public   class PropStaffBuyTermInput
    {
        public BabyProp BabyProp { get; set; }

        public int BabyId { get; set; }

        public int FamilyId { get; set; }

        public List<BabyPropBuyTermType> BabyPropBuyTermTypes { get; set; }
    }
}
