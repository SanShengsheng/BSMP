using MQKJ.BSMP.EnumHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MQKJ.BSMP.Questions
{
    /// <summary>
    /// 性别
    /// </summary>
 public   enum QuestionGender
    {
        /// <summary>
        /// 未知
        /// </summary>
        [EnumDescription("未知")]
        Unknown = -1,

        /// <summary>
        /// 男生版
        /// </summary>
        [EnumDescription("男生版")]
        M =0,

        /// <summary>
        /// 女生版
        /// </summary>
        [EnumDescription("女生版")]
        F = 1
    }
}
