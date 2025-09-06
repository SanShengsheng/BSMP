
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP;

namespace  MQKJ.BSMP.Dtos
{
    [AutoMapTo(typeof(MqAgent))]
    public class MqAgentEditDto : IAddModel<MqAgent, int>, IEditModel<MqAgent, int>
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
		/// <summary>
		/// TenantId
		/// </summary>
		public int? TenantId { get; set; }



		/// <summary>
		/// PlayerId
		/// </summary>
		public Guid PlayerId { get; set; }



		/// <summary>
		/// Level
		/// </summary>
		public AgentLevel Level { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [Required]
        public string IdCardNumber { get; set; }

        [Required]
        public string PhoneNumber { get; set; }



        /// <summary>
        /// State
        /// </summary>
        public AgentState State { get; set; }



		/// <summary>
		/// InviteCode
		/// </summary>
		public string InviteCode { get; set; }


        public string GroupId { get; set; }




    }
}