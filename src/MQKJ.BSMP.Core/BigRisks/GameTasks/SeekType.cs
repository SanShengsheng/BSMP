using MQKJ.BSMP.EnumHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.GameTasks
{
    public enum SeekType
    {
        /// <summary>
        /// 男追女
        /// </summary>
        [EnumDescription("女生主场")]
        ManSeekWoMan = 0,

        /// <summary>
        /// 女追男
        /// </summary>
        [EnumDescription("男生主场")]
        WoManSeekMan=1,

        /// <summary>
        /// 未知 //新flag爬梯不需要这个参数
        /// </summary>
        [EnumDescription("无")]
        None = -1
    }
}
