
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Common;

namespace  MQKJ.BSMP.Common.Dtos
{
    public class EnterpirsePaymentRecordEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// Amount
		/// </summary>
		public decimal Amount { get; set; }



		/// <summary>
		/// OutTradeNo
		/// </summary>
		public string OutTradeNo { get; set; }



		/// <summary>
		/// AgentId
		/// </summary>
		public int AgentId { get; set; }



		/// <summary>
		/// MqAgent
		/// </summary>
		public MqAgent MqAgent { get; set; }



		/// <summary>
		/// State
		/// </summary>
		public WithdrawDepositState State { get; set; }



		/// <summary>
		/// PaymentData
		/// </summary>
		public string PaymentData { get; set; }



		/// <summary>
		/// PaymentNo
		/// </summary>
		public string PaymentNo { get; set; }



		/// <summary>
		/// PaymentTime
		/// </summary>
		public DateTime PaymentTime { get; set; }




    }
}