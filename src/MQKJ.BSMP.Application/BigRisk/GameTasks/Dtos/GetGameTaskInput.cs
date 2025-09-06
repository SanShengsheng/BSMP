using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.GameTasks;
using System;

namespace MQKJ.BSMP.GameTasks.Dtos
{ 
    public class GetGameTasksInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 通关类型
        /// </summary>
        public int TaskType { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public int TaskState { get; set; }

        /// <summary>
        /// 被追求类型
        /// </summary>
        public int SeekType { get; set; }

        /// <summary>
        /// 关系类型
        /// </summary>
        public int RelationType { get; set; }

        /// <summary>
        /// 模糊搜索使用的关键字
        /// </summary>
        //public string Filter { get; set; }



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
