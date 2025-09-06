using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MQKJ.BSMP.GameRecords
{
    public enum GameState
    {
        /// <summary>
        /// 初始化状态
        /// </summary>
        InitializationState = 0,

        /// <summary>
        /// 准备状态
        /// </summary>
        ReadyState,

        /// <summary>
        /// 开始状态
        /// </summary>
        StartState,

        /// <summary>
        /// 结束状态
        /// </summary>
        EndState,


    }
}