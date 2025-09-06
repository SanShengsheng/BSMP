using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Tags;
using MQKJ.BSMP.TagTypes;

namespace MQKJ.BSMP.Tags.Dtos
{

    public class TagListDto : FullAuditedEntityDto
    {
  

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
        public  TagType TagType { get; set; }



        //// custom codes 





        //// custom codes end
    }
}