using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.EnumHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.Common.MqAgents
{
    [Table("AgentInviteCodes")]
    public class AgentInviteCode:FullAuditedEntity
    {
        public string Code { get; set; }

        public InviteCodeState State { get; set; }

        public int? MqAgentId { get; set; }

        public MqAgent MqAgent { get; set; }

        public MqAgentCategory MqAgentCategory { get; set; }
    }
    public enum MqAgentCategory
    {
        /// <summary>
        /// 一级代理
        /// </summary>
        [EnumDescription("一级代理")]
        AgentCategory = 1,

        /// <summary>
        /// 一级推广
        /// </summary>
        [EnumDescription("一级推广")]
        SpreadCategory = 2,

        /// <summary>
        /// 二级代理
        /// </summary>
        [EnumDescription("二级代理")]
        SecodeCategory = 3,

        /// <summary>
        /// 管理员
        /// </summary>
        [EnumDescription("管理员")]
        AdministratorCategory = 4
    }

    public enum InviteCodeState
    {

        /// <summary>
        /// 无
        /// </summary>
        [EnumDescription("无")]
        UnKnow = -1,


        /// <summary>
        /// 未用
        /// </summary>
        [EnumDescription("未用")]
        UnUseState = 1,

        /// <summary>
        /// 已用
        /// </summary>
        [EnumDescription("已用")]
        UsedState = 2
    }
}
