using MQKJ.BSMP.TagTypes.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Questions.Dtos
{
    public class QuestionTagListDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }


        /// <summary>
        /// 标签类别编号
        /// </summary>
        public int TagTypeId { get; set; }


        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; }


        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 标签类型
        /// </summary>
        public TagTypeDto TagType { get; set; }

    }
}
