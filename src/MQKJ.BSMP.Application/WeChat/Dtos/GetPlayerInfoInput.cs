using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using MQKJ.BSMP.WeChat.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MQKJ.BSMP.Players.WeChat.Dtos
{
    [AutoMapTo(typeof(PlayerInfoEditDto))]
    public class GetPlayerInfoInput
    {
        [Required]
        public string Code { get; set; }

        public string OpenId { get; set; }

        public string HeadUrl { get; set; }


        public string NickName { get; set; }
        public string DeviceModel { get; set; }

        public string DeviceSystem { get; set; }

        //public int Gender { get; set; }

        ///// <summary>
        ///// 小程序编号，默认为关系进化
        ///// </summary>
        //[Required]
        //public EWechatProgrammEnum WechatId { get; set; }

        public Guid? InviterId { get; set; }
    }

}
