using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MQKJ.BSMP.MqAgents.Dtos
{
    public class CreateAgentInput
    {
        [Required]
        public string PhoneNumber { get; set; }

        //[Required]
        //public string WechatAccount { get; set; }

            public string ValidCode { get; set; }

        public int? AgentId { get; set; }


        //public IFormFile FormFkile { get; set; }

        //public string GroupId { get; set; }

        public string InviteCode { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        //[Required]
        public string UserName { get; set; }

        //[Required]
        //public string Password { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        //[Required]
        public string IdCardNumber { get; set; }

        //public Guid? PlayerId { get; set; }

        public int? UserId { get; set; }


    }
}
