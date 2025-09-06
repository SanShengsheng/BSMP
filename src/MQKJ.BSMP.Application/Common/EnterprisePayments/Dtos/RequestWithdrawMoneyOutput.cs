using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.EnterprisePayments.Dtos
{
    public class RequestWithdrawMoneyOutput
    {
        public ErrorCode ErrorCode { get; set; }
    }

    public enum ErrorCode
    {
        /// <summary>
        /// 未审核
        /// </summary>
        UnAudite = 1,

        /// <summary>
        /// 账号冻结
        /// </summary>
        Frozen = 2,

        /// <summary>
        /// 提现金额必须大于1元
        /// </summary>
        NotEnough = 3,

        /// <summary>
        /// 超出限制
        /// </summary>
        ExceedingLimit = 4,

        /// <summary>
        /// 已有未处理的记录
        /// </summary>
        HadUnHandleRecord = 5
    }
}
