using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.PassModelStatistics.Dtos
{
    public class GetPassModelLevelDistributionDto: PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public DateTime DateTime { get; set; } = DateTime.Now;

        public void Normalize()
        {
            if (!string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            };
        }
    }
}
