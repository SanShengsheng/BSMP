using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Dtos
{
    public class GetOtherFamilyInfoInput
    {
        public int? BabyId { get; set; }

        public int FamilyId { get; set; }
        /// <summary>
        /// 当前宝宝ID
        /// </summary>
        public string NowBabyId { get; set; }
    }
}
