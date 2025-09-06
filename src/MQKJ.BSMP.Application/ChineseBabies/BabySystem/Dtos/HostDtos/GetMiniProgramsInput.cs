using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.BabySystem.Dtos.HostDtos
{
    public class GetMiniProgramsInput: PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public int FamilyId { get; set; }

        public Guid PlayerId { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }
    }
}
