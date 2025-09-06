using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Tags;
using MQKJ.BSMP.TagTypes;

namespace MQKJ.BSMP.Tags.Dtos
{
    [AutoMapTo(typeof(Tag))]
    public class TagEditDto : FullAuditedEntityDto
    {
        public new int? Id { get; set; }
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
        /// 标签类别
        /// </summary>
        public TagType TagType { get; set; }



        //// custom codes 

        //// custom codes end
    }
}