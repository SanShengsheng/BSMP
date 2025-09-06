using Abp.Runtime.Validation;
using MQKJ.BSMP.Common.AD;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.Adviertisements.Dtos
{
    public class GetAdviertisementsInput: PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public string Name { get; set; }

        public AdviertisementState AdviertisementState { get; set; }

        public AdviertisementPlatform AdviertisementPlatform { get; set; }

        public int? TeantId { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
