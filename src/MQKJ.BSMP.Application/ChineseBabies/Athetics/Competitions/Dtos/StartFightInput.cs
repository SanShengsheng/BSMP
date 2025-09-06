using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos
{
    public class StartFightInput
    {
        public Guid? PlayerId { get; set; }

        /// <summary>
        /// 发起者家庭Id
        /// </summary>
        public int? FamilyId { get; set; }

        /// <summary>
        /// 宝宝Id
        /// </summary>
        public int? BabyId { get; set; }

        /// <summary>
        /// 对方宝宝Id
        /// </summary>
        public int? OtherBabyId { get; set; }
    }
}
