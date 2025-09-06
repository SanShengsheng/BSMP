using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Players.WeChat.Dtos
{
    public class CreateOrUpdatePlayerInfoInput
    {
        public PlayerInfoEditDto PlayerInfoDto { get; set; }
    }
}
