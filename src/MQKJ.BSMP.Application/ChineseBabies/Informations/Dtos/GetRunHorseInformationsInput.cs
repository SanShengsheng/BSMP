using Abp.Runtime.Validation;
using MQKJ.BSMP.Common.RunHorseInformations;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Informations.Dtos
{
    public class GetRunHorseInformationsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {

        public PlayScene? PlayScene { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
