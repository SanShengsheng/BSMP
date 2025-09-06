

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.ChineseBabies;
using Abp.AutoMapper;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.Orders.Dtos
{
    [AutoMapFrom(typeof(Order))]
    public class OrderListDto
    {


        /// <summary>
        /// Payment
        /// </summary>
        public double Payment { get; set; }

        public DateTime CreationTime { get; set; }



        public bool IsWithdrawCash { get; set; }

        /// <summary>
        /// ProductDescribe
        /// </summary>
        public string ProductDescribe { get; set; }


        //public Player Player { get; set; }

            public OrderPlayer Player { get; set; }



        /// <summary>
        /// PaymentType
        /// </summary>
        public int PaymentType { get; set; }



        /// <summary>
        /// OrderNumber
        /// </summary>
        public string OrderNumber { get; set; }


        public GoodsType GoodsType { get; set; }



        /// <summary>
        /// PaymentTime
        /// </summary>
        public DateTime? PaymentTime { get; set; }



        /// <summary>
        /// State
        /// </summary>
        public OrderState State { get; set; }


        public string TransactionId { get; set; }


        /// <summary>
        /// PlayerId
        /// </summary>
        public Guid PlayerId { get; set; }

        public int? FamilyId { get; set; }

        //public Family Family { get; set; }


        public int TenantId { get; set; }

        public string TenantName { get; set; }

        public string AgentName { get; set; }

        public OrderFamily Family { get; set; }

    }

    [AutoMapFrom(typeof(Player))]
    public class OrderPlayer
    {
        public string NickName { get; set; }

        public Guid Id { get; set; }

        public bool IsAgenter { get; set; }
    }

    [AutoMapFrom(typeof(Family))]
    public class OrderFamily
    {
        public int Id { get; set; }

        public OrderPlayer Father { get; set; }


        public OrderPlayer Mother { get; set; }

        public Guid FatherId { get; set; }

        public Guid MotherId { get; set; }
    }
}