
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP;
using System;

namespace MQKJ.BSMP.Dtos
{
    public class GetMqAgentsInput : PagedSortedAndFilteredInputDto, IShouldNormalize, ISearchModel<MqAgent, int>
    {

        public string UserName { get; set; }

        public AgentLevel? AgentLevel { get; set; }
        /// <summary>
        /// 上级代理名称
        /// </summary>
        public string UpperAgentNickName { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public AgentState? AgentState { get; set; }

        public string SourceName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 推广提现比例参数
        /// </summary>
        public string PromoterWithdrawalRatio { get; set; }
        /// <summary>
        /// 代理提现比例
        /// </summary>
        public string AgentWithdrawalRatio { get; set; }
        /// <summary>
        /// 所属公司
        /// </summary>
        public string Company { get; set; }
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
