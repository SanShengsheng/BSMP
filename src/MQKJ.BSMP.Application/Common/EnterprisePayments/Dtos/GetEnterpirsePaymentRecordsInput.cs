
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.Common;
using System;

namespace MQKJ.BSMP.Common.Dtos
{
    public class GetEnterpirsePaymentRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {

        public DateTime? StartTime { get; set; }


        public DateTime? EndTime { get; set; }

        public WithdrawDepositState WithdrawDepositState { get; set; }

        public string UserName { get; set; }

        public WithdrawMoneyType WithdrawMoneyType { get; set; }

        public string OrderNumber { get; set; }


        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
        }

    }
}
