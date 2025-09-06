using System;
using System.Collections.Generic;
using System.Text;
using MQKJ.BSMP.EnumHelper;

namespace MQKJ.BSMP.GameTasks
{
    public enum TaskType
    {
        /// <summary>
        /// none
        /// </summary>
        [EnumDescription("无")]
        None = 0,

        /// <summary>
        /// 三题关
        /// </summary>
        [EnumDescription("三题关")]
        ThreeQuesion = 3,

        /// <summary>
        /// 五题关
        /// </summary>
        [EnumDescription("五题关")]
        FiveQuestion = 5,

        /// <summary>
        /// 十题关
        /// </summary>
        [EnumDescription("十题关")]
        TenQuestion = 10

    }
}
