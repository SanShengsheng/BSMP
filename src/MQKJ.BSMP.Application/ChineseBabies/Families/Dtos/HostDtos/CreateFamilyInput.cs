using System;

namespace MQKJ.BSMP.ChineseBabies.HostDtos.FamilyDto
{
    public class CreateFamilyInput
    {
        /// <summary>
        /// 创建者编号
        /// </summary>
        public Guid PlayerGuid { get; set; }
        /// <summary>
        /// 邀请方编号
        /// </summary>
        public Guid InviterPlayerGuid { get; set; }
        /// <summary>
        /// 邀请者家庭身份
        /// </summary>
        public FamilyIdentity InviterFamilyIdentity { get; set; }
    }
    /// <summary>
    ///  家庭身份 
    /// </summary>
    public enum FamilyIdentity
    {
        /// <summary>
        /// 爸爸
        /// </summary>
        Dad=1,
        /// <summary>
        /// 妈妈
        /// </summary>
        Mom=2
    }
}
