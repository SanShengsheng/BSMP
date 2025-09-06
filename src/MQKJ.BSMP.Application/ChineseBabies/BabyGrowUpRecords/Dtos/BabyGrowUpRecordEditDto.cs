using Abp.AutoMapper;
using System;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapTo(typeof(BabyGrowUpRecord))]
    [AutoMapFrom(typeof(BabyGrowUpRecord))]
    public class BabyGrowUpRecordEditDto : BabyPropertyBase<Guid?>
    {
        /// <summary>
        /// BabyId
        /// </summary>
        public int BabyId { get; set; }

        public Guid? PlayerId { get; set; }

        /// <summary>
        /// ¥•∑¢¿‡–Õ
        /// </summary>
        public TriggerType TriggerType { get; set; }

    }
}