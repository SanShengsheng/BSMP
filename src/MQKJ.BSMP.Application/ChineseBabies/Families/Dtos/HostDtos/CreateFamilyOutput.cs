using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies.HostDtos.FamilyDto
{
    public class CreateFamilyOutput
    {
        /// <summary>
        /// 宝宝编号
        /// </summary>
        public int BabyId { get; set; }
        /// <summary>
        /// 家庭编号
        /// </summary>
        public int FamilyId { get; set; }
    }
}
