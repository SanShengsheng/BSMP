using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Players
{
    public enum PlayerState
    {

        /// <summary>
        /// 已授权
        /// </summary>
        Authorized = 0,

        /// <summary>
        /// 未授权
        /// </summary>
        Unauthorize,

        /// <summary>
        /// 冻结
        /// </summary>
        Freeze

    }
}
