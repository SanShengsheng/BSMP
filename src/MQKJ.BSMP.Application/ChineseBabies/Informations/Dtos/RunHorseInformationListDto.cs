using Abp.AutoMapper;
using MQKJ.BSMP.Common.RunHorseInformations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Informations.Dtos
{
    [AutoMapFrom(typeof(RunHorseInformation))]
    public class RunHorseInformationListDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 开始时间 null表示无限播放
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间 null表示无限播放
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 播放间隔  默认3秒
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// 播放次数  -1表示无（（）限播放 默认-1
        /// </summary>
        public int PlayCount { get; set; }

        /// <summary>
        /// 播放场景
        /// </summary>
        public PlayScene PlayScene { get; set; }
    }
}
