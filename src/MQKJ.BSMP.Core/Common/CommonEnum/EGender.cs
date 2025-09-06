using MQKJ.BSMP.EnumHelper;

namespace MQKJ.BSMP.CommonEnum
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum EGender
    {
        /// <summary>
        /// 未知
        /// </summary>
        [EnumDescription("未知")]
        Unknown = 0,

        /// <summary>
        /// 男
        /// </summary>
        [EnumDescription("男")]
        M = 1,

        /// <summary>
        /// 女
        /// </summary>
        [EnumDescription("女")]
        F = 2
    }
}
