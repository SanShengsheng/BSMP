
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.SystemMessages;
using System;

namespace MQKJ.BSMP.SystemMessages.Dtos
{
    public class GetSystemMessagesInput : PagedSortedAndFilteredInputDto, IShouldNormalize, ISearchModel<SystemMessage, int>
    {
        /// <summary>
        /// NoticeType
        /// </summary>
        public NoticeType NoticeType { get; set; }



        /// <summary>
        /// PeriodType
        /// </summary>
        public PeriodType PeriodType { get; set; }



        /// <summary>
        /// PriorityLevel
        /// </summary>
        public int PriorityLevel { get; set; }



        /// <summary>
        /// ExprieDateTime
        /// </summary>
        public DateTime ExprieDateTime { get; set; }



        /// <summary>
        /// StartDateTime
        /// </summary>
        public DateTime StartDateTime { get; set; }
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
