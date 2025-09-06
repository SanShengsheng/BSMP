using Abp.AutoMapper;
using MQKJ.BSMP.Emoticons;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    [AutoMapTo(typeof(EmoticonRecord))]
    public class SendEmoticonDto
    {
        /// <summary>
        /// 表情编号
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 玩家Id
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 房间Id
        /// </summary>
        public Guid GameTaskId { get; set; }
    }
}
