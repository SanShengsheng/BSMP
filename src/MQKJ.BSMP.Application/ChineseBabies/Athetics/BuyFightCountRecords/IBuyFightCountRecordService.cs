using MQKJ.BSMP.ChineseBabies.Athetics.BuyFightCountRecords.Dtos;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos;
using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies.Athetics.BuyFightCountRecords
{
    interface IBuyFightCountRecordService : BsmpApplicationService<BuyFightCountRecord,Guid, BuyFightCountRecordEditDto, BuyFightCountRecordEditDto, GetBuyFightCountRecordInput, BuyFightCountRecordListDtos>
    {
        //Task<BuyFightCountOutput> BuyFightCount(BuyFightCountRecordInput input);
    }
}
