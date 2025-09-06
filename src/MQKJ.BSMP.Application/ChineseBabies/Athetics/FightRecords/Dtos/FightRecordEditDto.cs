using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.FightRecords.Dtos
{
    public class FightRecordEditDto : IAddModel<FightRecord, Guid>, IEditModel<FightRecord, Guid>
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }
    }
}
