using System.Collections.Generic;
using Abp.Runtime.Validation;
using JetBrains.Annotations;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.Questions;

namespace MQKJ.BSMP.Questions.Dtos
{
    public class GetQuestionsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        /// <summary>
        /// 模糊搜索使用的关键字
        /// </summary>
        public string Filter { get; set; }

        //// custom codes 

        /// <summary>
        /// 场景编号
        /// </summary>
        public int SceneId { get; set; }

        /// <summary>
        /// 被追求性别
        /// </summary>
        public QuestionGender? PursuingGender { get; set; } = QuestionGender.Unknown;

        /// <summary>
        /// 问题状态
        /// </summary>
        public QuestionState? State { get; set; } = QuestionState.WaitConfirm;

        [CanBeNull] public  List<int> Tags { get; set; }

        public int CreatorId { get; set; }

        ///// <summary>
        ///// 用户关系类别
        ///// </summary>
        //public int RelationType { get; set; }

        ///// <summary>
        ///// 私密度
        ///// </summary>
        //public int PrivateDensity { get; set; }

        ///// <summary>
        ///// 年龄层
        ///// </summary>
        //public int AgeRange { get; set; }

        ///// <summary>
        ///// 话题
        ///// </summary>
        //public string Topic { get; set; }


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
