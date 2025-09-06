using System;
using System.Collections.Generic;
using System.Text;
using MQKJ.BSMP.EnumHelper;

namespace MQKJ.BSMP.GameTasks
{
    /// <summary>
    /// 关系维度
    /// </summary>
    public enum RelationDegree
    {
        /// <summary>
        /// None
        /// </summary>
        [EnumDescription("无")]
        None = 0,

        /// <summary>
        /// 普通
        /// </summary>
        [EnumDescription("普通")]
        Ordinary = 1,

        /// <summary>
        /// 暧昧
        /// </summary>
        [EnumDescription("暧昧")]
        Ambiguous,

        /// <summary>
        /// 情侣
        /// </summary>
        [EnumDescription("情侣")]
        Lovers,

        /// <summary>
        /// 夫妻
        /// </summary>
        [EnumDescription("夫妻")]
        Couple

    }
}
