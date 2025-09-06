using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using MQKJ.BSMP.GameTasks;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using MQKJ.BSMP.WeChat.Dtos;

namespace MQKJ.BSMP.Players.WeChat.Dtos
{
    [AutoMapTo(typeof(GameTask))]
    public class ReadyGameInput:EntityDto<Guid>
    {
        ///// <summary>
        ///// 被邀请方Id
        ///// </summary>
        //public Guid InviteePlayerId { get; set; }

        public TaskState State { get; set; }

    }
}
