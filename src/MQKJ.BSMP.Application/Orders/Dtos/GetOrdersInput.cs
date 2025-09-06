
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.Orders;
using System;

namespace MQKJ.BSMP.Orders.Dtos
{
    public class GetOrdersInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {

        public string UserName { get; set; }

        public OrderState OrderState { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        /// <summary>
        /// 商户号或微信订单号
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public PaymentType PaymentType { get; set; }

        public double Amount { get; set; }

        public int TenantId { get; set; }

        public int AgentCount { get; set; }

        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime desc";
            }
        }

    }
}
