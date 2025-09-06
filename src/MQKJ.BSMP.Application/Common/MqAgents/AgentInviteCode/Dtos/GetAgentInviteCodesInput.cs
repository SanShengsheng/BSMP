
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.Common.MqAgents;

namespace MQKJ.BSMP.Common.MqAgents.Dtos
{
    public class GetAgentInviteCodesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public MqAgentCategory MqAgentCategory { get; set; }


        public InviteCodeState InviteCodeState { get; set; }


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
