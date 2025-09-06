

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Common.MqAgents;
using Abp.AutoMapper;
using MQKJ.BSMP.MqAgents.Dtos;

namespace MQKJ.BSMP.Common.MqAgents.Dtos
{
    [AutoMapFrom(typeof(AgentInviteCode))]
    public class AgentInviteCodeListDto 
    {

        
		/// <summary>
		/// Code
		/// </summary>
		public string Code { get; set; }



		/// <summary>
		/// State
		/// </summary>
		public InviteCodeState State { get; set; }



		/// <summary>
		/// AgentId
		/// </summary>
		public int MqAgentId { get; set; }



		/// <summary>
		/// MqAgent
		/// </summary>
		public MqAgentDto MqAgent { get; set; }

        public DateTime CreationTime { get; set; }



		/// <summary>
		/// firstAgentCategory
		/// </summary>
		public MqAgentCategory MqAgentCategory { get; set; }

    }

    //[AutoMapFrom(typeof(MqAgent))]
    //public class MqAgentDto
    //{
    //    public string HeadUrl { get; set; }

    //    public string NickName { get; set; }
    //}
}