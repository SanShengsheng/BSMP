using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Players.WeChat.Dtos
{
    [AutoMapTo(typeof(Player))]
    public class PlayerInfoEditDto : EntityDto<Guid>
    {

        public new Guid? Id { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像url
        /// </summary>
        public string HeadUrl { get; set; }

        /// <summary>
        /// 玩家状态  0授权 1未授权 2正常 3冻结 
        /// </summary>
        public int State { get; set; }

        public int Gender { get; set; }

        /// <summary>
        /// 玩家年龄层次
        /// </summary>
        public int AgeRange { get; set; }

        public DateTime AuthorizeDateTime { get; set; }
    }
}
