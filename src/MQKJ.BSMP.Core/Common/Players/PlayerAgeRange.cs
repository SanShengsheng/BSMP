using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Players
{
    public enum PlayerAgeRange
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKnown,

        /// <summary>
        /// 学生族 22以下
        /// </summary>
        Student = 1,

        /// <summary>
        /// 上班族 22-28
        /// </summary>
        Worker,

        /// <summary>
        /// 其它 28以上
        /// </summary>
        Other

    }
}
