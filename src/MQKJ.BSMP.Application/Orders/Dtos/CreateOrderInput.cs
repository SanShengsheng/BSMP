using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MQKJ.BSMP.Orders.Dtos
{
    [AutoMapTo(typeof(Order))]
    public class CreateOrderInput
    {



        /// <summary>
        /// Payment
        /// </summary>
        [Required]
        public double Payment { get; set; }



        /// <summary>
        /// ProductDescribe
        /// </summary>
        public string ProductDescribe { get; set; }



        /// <summary>
        /// PaymentType
        /// </summary>
        public PaymentType PaymentType { get; set; }



        /// <summary>
        /// OrderNumber
        /// </summary>
        [Required]
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
        [Required]
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        public virtual int? ProductId { get; set; }


        public int TenantId { get; set; }

        public GoodsType GoodsType { get; set; }
        public int? FamilyId { get; set; }

        public Guid? PropBagId { get; set; }
        /// <summary>
        /// 对应的应用ID
        /// </summary>
        public string App_ID { get; set; }

        /// <summary>
        ///  商户ID
        /// </summary>
        public string Seller_ID { get; set; }
    }
}
