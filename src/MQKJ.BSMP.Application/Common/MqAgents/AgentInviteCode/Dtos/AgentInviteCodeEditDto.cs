
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Common.MqAgents;

namespace  MQKJ.BSMP.Common.MqAgents.Dtos
{
    [AutoMapTo(typeof(AgentInviteCode))]
    public class AgentInviteCodeEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
		/// <summary>
		/// Code
		/// </summary>
		public string Code { get; set; }



        /// <summary>
        /// State
        /// </summary>
        public InviteCodeState State { get; set; } = InviteCodeState.UnUseState;



        /// <summary>
        /// AgentId
        /// </summary>
        public int? MqAgentId { get; set; }



		/// <summary>
		/// MqAgent
		/// </summary>
		public MqAgent MqAgent { get; set; }



		/// <summary>
		/// firstAgentCategory
		/// </summary>
		public MqAgentCategory MqAgentCategory { get; set; }




    }
}