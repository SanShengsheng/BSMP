using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.EnterprisePayments.Dtos
{
    public class WithDrawMoneyRecordGetToExcelInput
    {
        public DateTime? StartTime { get; set; }


        public DateTime? EndTime { get; set; }

        public WithdrawDepositState WithdrawDepositState { get; set; }

        public string UserName { get; set; }

        public WithdrawMoneyType WithdrawMoneyType { get; set; }

        public string OrderNumber { get; set; }
    }
}
