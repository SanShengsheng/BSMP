using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.EnterprisePayments.Dtos
{
    [AutoMapFrom(typeof(EnterpirsePaymentRecord))]
    public class GetAgentWithdrawMoneyRecordOutput
    {
        public DateTime CreationTime { get; set; }

        public decimal Amount { get; set; }

        public WithdrawDepositState State { get; set; }

        public DateTime PaymentTime { get; set; }

        public string OutTradeNo { get; set; }
    }
}
