using MQKJ.BSMP.EnumHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Questions
{
    public enum QuestionState : byte
    {
        /// <summary>
        /// 冻结状态
        /// </summary>
        [EnumDescription("已下线")]
        Freeze = 1,

        /// <summary>
        /// 激活状态，默认状态
        /// </summary>
        [EnumDescription("待审核")]
        WaitConfirm = 0,

        /// <summary>
        /// 已审核
        /// </summary>
        [EnumDescription("已审核")]
        Confrimed = 2,

        /// <summary>
        /// 所有
        /// </summary>
        [EnumDescription("所有")]
        All = 3,

        /// <summary>
        /// 与冻结状态相比
        /// </summary>
        [EnumDescription("已上线")]
        Online=4,

    
    }
}
