
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Orders;

namespace  MQKJ.BSMP.Orders.Dtos
{
    public class OrderEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// Payment
		/// </summary>
		public double Payment { get; set; }



		/// <summary>
		/// ProductDescribe
		/// </summary>
		public string ProductDescribe { get; set; }



		/// <summary>
		/// PaymentType
		/// </summary>
		public int PaymentType { get; set; }



		/// <summary>
		/// OrderNumber
		/// </summary>
		public string OrderNumber { get; set; }



		/// <summary>
		/// PaymentTime
		/// </summary>
		public DateTime? PaymentTime { get; set; }



		/// <summary>
		/// State
		/// </summary>
		public OrderState State { get; set; }



		/// <summary>
		/// PlayerId
		/// </summary>
		public Guid PlayerId { get; set; }


        public int? FamilyId { get; set; }

        public Family Family { get; set; }

    }
}