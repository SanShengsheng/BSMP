using Abp.Runtime.Validation;
using MQKJ.BSMP.ChineseBabies.Athletics;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.FightRecords.Dtos
{
    public class GetFightRecordInput : PagedSortedAndFilteredInputDto, IShouldNormalize, ISearchModel<FightRecord, Guid>
    {
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
