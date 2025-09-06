using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.GamblingModelStatistics.Dtos
{
    public class GetErrQuestionDistributionDto : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public DateTime DateTime { get; set; }

        public void Normalize()
        {
            if (!string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            };
        }
    }
}
