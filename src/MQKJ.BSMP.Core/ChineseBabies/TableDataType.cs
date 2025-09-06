using MQKJ.BSMP.EnumHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    public enum TableDataType
    {
        [EnumDescription("奖励")]
        /// <summary>
        /// 奖励
        /// </summary>
        Reward = 0,
        [EnumDescription("消耗")]
        /// <summary>
        /// 消耗
        /// </summary>
        Consume = 1,
        [EnumDescription("事件和选项")]
        /// <summary>
        /// 事件和选项
        /// </summary>
        BabyEventAndOptions = 2,
        [EnumDescription("组")]
        /// <summary>
        /// 组
        /// </summary>
        EventGroup = 3,
        [EnumDescription("结局")]
        /// <summary>
        /// 结局
        /// </summary>
        Ending = 4,
        [EnumDescription("职业表")]
        /// <summary>
        /// 职业表
        /// </summary>
        Profession = 5,
        [EnumDescription("道具表")]

        /// <summary>
        /// 道具表
        /// </summary>
        BabyProp = 6,
        [EnumDescription("属性奖励表")]
        /// <summary>
        /// 属性奖励表
        /// </summary>
        PropertyAward = 7,
        [EnumDescription("大礼包")]
        /// <summary>
        /// 大礼包
        /// </summary>
        BigGiftBag = 8,
    }
}
