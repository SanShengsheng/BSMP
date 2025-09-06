using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.BuyFightCountRecords.Dtos
{
    public class BuyFightCountRecordEditDto : IAddModel<BuyFightCountRecord, Guid>, IEditModel<BuyFightCountRecord, Guid>
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }
    }
}
