using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.BonusPoints;
using System;
using System.ComponentModel;

namespace MQKJ.BSMP.BonusPointRecords.Dtos
{ 
    public class GetBonusPointRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        /// <summary>
        /// 模糊搜索使用的关键字
        /// </summary>
        //public string Filter { get; set; }


        public string UserName { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int EventId { get; set; }

        //// custom codes 

        //// custom codes end

        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }

       
    }
}
