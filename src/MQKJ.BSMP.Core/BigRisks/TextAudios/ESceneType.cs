using MQKJ.BSMP.EnumHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.TextAudios
{
    public enum ESceneType
    {
        [EnumDescription("默认")]
        Default = 0,
        [EnumDescription("抽取约定")]
        Flag = 1,
        [EnumDescription("答题")]
        Answer = 2
    }
}
