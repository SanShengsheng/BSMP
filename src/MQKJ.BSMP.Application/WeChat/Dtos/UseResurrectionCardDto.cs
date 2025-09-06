using Abp.AutoMapper;
using MQKJ.BSMP.PropUseRecords;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    [AutoMapTo(typeof(PropUseRecord))]
    public class UseResurrectionCardDto
    {
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 道具类型
        /// </summary>
        public PropType PropType { get; set; }
    }
}
