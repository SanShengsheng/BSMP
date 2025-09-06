using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Dtos
{
    //[AutoMap(typeof(MqAgent))]
    public class UpdateAgentInfoInput
    {
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        //[Required]
        public string UserName { get; set; }


        /// <summary>
        /// 身份证号
        /// </summary>
        //[Required]
        public string IdCardNumber { get; set; }
        public string AliPayAccount { get;  set; }

        /// <summary>
        /// 银行卡开户卡号
        /// </summary>
        public string CardNo { get; set; }
    }
}
