using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.MqAgents.Dtos
{
    public class GetMoneyDetailedInput: PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        //public Guid PlayerId { get; set; }
        public int? AgentId { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
