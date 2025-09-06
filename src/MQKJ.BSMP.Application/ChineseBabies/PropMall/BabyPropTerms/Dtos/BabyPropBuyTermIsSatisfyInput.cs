using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.PropMall.BabyPropTerms.Dtos
{
   public class BabyPropBuyTermIsSatisfyInput
    {
        public int BabyId { get; set; }

        public int FamilyId { get; set; }

        public BabyProp BabyProp { get; set; }

        public BabyPropTerm Term { get; set; }


        public BabyPropBuyTermType BabyPropBuyTermType { get; set; }
    }
}
