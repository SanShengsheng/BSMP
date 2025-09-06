using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.MqAgents.Dtos
{
    [AutoMapFrom(typeof(MqAgent))]
    public class CreateAgentOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }



        /// <summary>
        /// 用户名
        /// </summary>
        //public string UserName { get; set; }

        ///// <summary>
        ///// 身份证号
        ///// </summary>
        //public string IdCardNumber { get; set; }

        //public string PhoneNumber { get; set; }

        //public Agenter Player { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public AgentState State { get; set; }

        public AgentLevel AgentLevel { get; set; }



        //public string GroupId { get; set; }


        public CreateAgentErrCode ErrorCode { get; set; }

    }

    public class Agenter
    {
        public string WeChatAccount { get; set; }
    }

    public enum CreateAgentErrCode
    {
        /// <summary>
        /// 已注册
        /// </summary>
        Registered = 1,

        /// <summary>
        /// 未玩过
        /// </summary>
        UnPlayed =  2,

        /// <summary>
        /// 验证码错误
        /// </summary>
        ValidCodeErr = 3,

        /// <summary>
        /// 无效验证码
        /// </summary>
        InvalidInviteCode = 4,

        /// <summary>
        /// 已使用
        /// </summary>
        InivteCodeUsed = 5
    }
}
