

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Common;
using Abp.AutoMapper;
using Newtonsoft.Json;
using MQKJ.BSMP.Utils.WechatPay.Modes;
using Newtonsoft.Json.Linq;

namespace MQKJ.BSMP.Common.Dtos
{
    [AutoMapFrom(typeof(EnterpirsePaymentRecord))]
    public class EnterpirsePaymentRecordListDto
    {
        public Guid Id { get; set; }

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


        public DateTime CreationTime { get; set; }

        //private string _paymentNo;
        /// <summary>
        /// PaymentNo
        /// </summary>
        public string PaymentNo
        {
            get; set;
        }



        /// <summary>
        /// PaymentTime
        /// </summary>
        public DateTime PaymentTime { get; set; }

        public WithdrawMoneyType WithdrawMoneyType { get; set; }

        /// <summary>
        /// 申请支付平台，用户提现申请请求使用哪一个平台
        /// </summary>
        public WithdrawMoneyType RequestPlatform { get; set; }
    }
}