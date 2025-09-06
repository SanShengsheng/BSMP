using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class GetPosterInfoOutput
    {
        /// <summary>
        ///成好友的天数
        /// </summary>
        public int FriendDays { get; set; }

        /// <summary>
        /// 经历的场景
        /// </summary>
        public int SceneCount { get; set; }


        /// <summary>
        /// 一天内闯关数
        /// </summary>
        public int BarrierCount { get; set; }

        /// <summary>
        /// 话题数
        /// </summary>
        public int TopicCount { get; set; }

        /// <summary>
        /// 话题
        /// </summary>
        public string TopicName { get; set; }
    }
}
