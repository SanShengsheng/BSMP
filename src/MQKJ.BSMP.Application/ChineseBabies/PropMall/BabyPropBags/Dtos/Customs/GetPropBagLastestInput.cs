using System;

namespace MQKJ.BSMP.ChineseBabies.PropMall
{
    public class GetPropBagLastestInput
    {
        public int BabyId { get; set; }

        public Guid PlayerGuid { get; set; }

        public int FamilyId { get; set; }
        /// <summary>
        ///  父母身份，如father,mother
        /// </summary>
        public  Gender ParentIden { get; set; }
    }
}