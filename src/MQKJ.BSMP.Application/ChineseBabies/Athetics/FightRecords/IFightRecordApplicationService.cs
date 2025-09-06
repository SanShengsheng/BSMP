using MQKJ.BSMP.ChineseBabies.Athetics.FightRecords.Dtos;
using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.FightRecords
{
    interface IFightRecordApplicationService: BsmpApplicationService<FightRecord,Guid,FightRecordEditDto, FightRecordEditDto, GetFightRecordInput,FightRecordListDtos>
    {
        
    }
}
