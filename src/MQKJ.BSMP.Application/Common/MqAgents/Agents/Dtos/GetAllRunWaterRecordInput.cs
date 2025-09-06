using Abp.Runtime.Validation;
using MQKJ.BSMP.Common.IncomeRecords;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Dtos
{
    public class GetAllRunWaterRecordInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public DateTime? StartTime { get; set; }


        public DateTime? EndTime { get; set; }

        public double? Amount { get; set; }

        //收益人
        //public string UserName { get; set; }

        /// <summary>
        /// 充值人
        /// </summary>
        public string RechargerName { get; set; }

        /// <summary>
        /// 业务人
        /// </summary>
        public string BusinesserName { get; set; }

        /// <summary>
        /// 收益人
        /// </summary>
        public string ProfiterName { get; set; }

        public string OrderNo { get; set; }

        public bool IsAll { get; set; }
        /// <summary>
        /// 流水类型，正常/补贴
        /// </summary>
        public IncomeTypeEnum RunWaterType { get; set; }
        public string Company { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
