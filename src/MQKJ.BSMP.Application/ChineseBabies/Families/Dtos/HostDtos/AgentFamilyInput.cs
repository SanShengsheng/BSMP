using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Dtos.HostDtos
{
    public class AgentFamilyInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public double? MinMoney { get; set; }
        public double? MaxMoney { get; set; }
        public AddOnStatus? Status { get; set; }
        public string NickName { get; set; }
        public Guid? PlayerId { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }
    }
}
