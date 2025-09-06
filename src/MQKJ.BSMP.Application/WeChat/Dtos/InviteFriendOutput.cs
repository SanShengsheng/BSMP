using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using MQKJ.BSMP.GameTasks;
using System;
using System.Collections.Generic;
using System.Text;
using MQKJ.BSMP.WeChat.Dtos;

namespace MQKJ.BSMP.Players.WeChat.Dtos
{
    [AutoMapFrom(typeof(GameTask))]
    public class InviteFriendOutput
    {
        /// <summary>
        /// 任务编号(房间号)
        /// </summary>
        //public Guid GameTaskId { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        //public DateTime InvalidTime { get; set; }
        /// <summary>
        /// 邀请链接
        /// </summary>
        public string InviteUrl { get; set; }

        /// <summary>
        /// 邀请语
        /// </summary>
        public string InviteMsg { get; set; }

        /// <summary>
        /// 邀请图片地址
        /// </summary>
        public string ImageUrl { get; set; }

   public  string ConnectionId { get; set; }
    }
}
