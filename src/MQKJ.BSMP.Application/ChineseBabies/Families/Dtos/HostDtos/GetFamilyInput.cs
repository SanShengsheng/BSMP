using System;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 获取家庭信息
    /// </summary>
    public class GetFamilyInput
    {
        /// <summary>
        /// 家庭编号
        /// </summary>
        public int FamilyId { get; set; }

        public Guid? PlayerGuid { get; set; }
    }
}