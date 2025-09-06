using Abp.Runtime.Validation;
using MQKJ.BSMP.ChineseBabies.Athletics;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos
{
    public class GetAthleticsInformationsInput :PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public Guid? PlayerId { get; set; }

        public int? FamilyId { get; set; }

        public AthleticsInformationType? Type { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
