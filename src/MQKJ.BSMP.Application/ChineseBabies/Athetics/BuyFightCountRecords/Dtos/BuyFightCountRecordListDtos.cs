using Abp.AutoMapper;
using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.BuyFightCountRecords.Dtos
{
    [AutoMapFrom(typeof(BuyFightCountRecord))]
    public class BuyFightCountRecordListDtos : ISearchOutModel<BuyFightCountRecord, Guid>
    {

    }
}
