using MQKJ.BSMP.PropUseRecords;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class UsePropOutput
    {

        public MsgCodeEnum MsgCodeEnum { get; set; }

        public PropType PropType { get; set; }

        /// <summary>
        /// 使用道具的玩家编号
        /// </summary>
        public Guid PlayerId { get; set; }
        /// <summary>
        /// 增减积分量
        /// </summary>
        //public int BonusPointCount { get; set; }
    }
}
