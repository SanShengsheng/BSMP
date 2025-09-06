using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.ChineseBabies
{
    public class GetFamilyElseBabiesByPageOutput
    {
        /// <summary>
        /// 宝宝数组
        /// </summary>
        public PagedResultDto<GetFamilyElseBabiesByPageOutputBaby> Babies { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpireTime { get; set; }

        public string Content { get; set; }

        /// <summary>
        /// 发起方Id
        /// </summary>
        public Guid? InitiatorId { get; set; }

    }
    [AutoMapTo(typeof(Baby))]
    public class GetFamilyElseBabiesByPageOutputBaby
    {
        /// <summary>
        /// 宝宝编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 第几胎
        /// </summary>
        public int BirthOrder { get; set; }
        /// <summary>
        /// 宝宝属性
        /// </summary>
        public GetFamilyElseBabiesByPageOutputBabyBabyProperty BabyProperty { get; set; }
        public GetFamilyElseBabiesByPageOutputBabyBabyStory StroyEnding { get; set; }
    }

    public class GetFamilyElseBabiesByPageOutputBabyBabyProperty: BabyPropertyDto
    {
    }
    [AutoMapFrom(typeof(BabyEnding))]
    public class GetFamilyElseBabiesByPageOutputBabyBabyStory 
    {
        public string Name { get; set; }
        public string Description { get; set; }
      
        public string Image { get; set; }
    }
}