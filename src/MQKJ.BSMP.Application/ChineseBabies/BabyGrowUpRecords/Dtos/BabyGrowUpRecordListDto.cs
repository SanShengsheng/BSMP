using Abp.AutoMapper;
using MQKJ.BSMP.ChineseBabies.BabyPropertyBaseDtos;
using System;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapTo(typeof(BabyGrowUpRecord))]
    public class BabyGrowUpRecordListDto : BabyPropertyBaseDto<Guid>
    {
        /// <summary>
        /// BabyId
        /// </summary>
        public int BabyId { get; set; }

        public Guid? PlayerId { get; set; }
    }
}