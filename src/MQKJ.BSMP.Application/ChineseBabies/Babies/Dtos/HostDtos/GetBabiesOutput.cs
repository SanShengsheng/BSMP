using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;

namespace MQKJ.BSMP.ChineseBabies
{
    public class GetBabiesOutput
    {
        /// <summary>
        /// 宝宝数组
        /// </summary>
        public List<GetBabiesDto> Babies { get; set; }

        public Guid PlayerId { get; set; }

    }
    public class GetBabiesByPageOutput
    {
        /// <summary>
        /// 宝宝数组
        /// </summary>
        public PagedResultDto<GetBabiesDto> Babies { get; set; }

        public Guid PlayerId { get; set; }

    }
    public class GetBabiesDto
    {
        /// <summary>
        /// 家庭信息
        /// </summary>
        public GetBabiesOutputFamily Family { get; set; }
        public GetBabiesOutputBaby Baby { get; set; }
    }
    /// <summary>
    /// 宝宝信息
    /// </summary>
    [AutoMapTo(typeof(Baby))]
    public class GetBabiesOutputBaby
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
        /// 年龄
        /// </summary>
        public string AgeString { get; set; }
          
        /// <summary>
        /// 第几胎
        /// </summary>
        public int BirthOrder { get; set; }
        /// <summary>
        /// 宝宝状态
        /// </summary>
        public BabyState State { get; set; }
        /// <summary>
        /// 爸爸是否查看宝宝出生动画
        /// </summary>
        public bool IsWatchBirthMovieFather { get; set; }
        /// <summary>
        /// 妈妈是否查看宝宝出生动画
        /// </summary>
        public bool IsLoadBirthMovieMother { get; set; }
    }
    public class GetBabiesOutputFamily
    {
        /// <summary>
        /// 家庭编号
        /// </summary>
        public int FamilyId { get; set; }
        /// <summary>
        /// 老爹
        /// </summary>
        public GetBabiesOutputParent Dad { get; set; }
        /// <summary>
        /// 老妈
        /// </summary>
        public GetBabiesOutputParent Mom { get; set; }
        /// <summary>
        /// 是否存在未成人的宝宝
        /// </summary>
        public bool IsHasUnderAgeBaby { get; set; }
    }
    public class GetBabiesOutputParent
    {
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadPicture { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 玩家编号
        /// </summary>
        public Guid PlayerGuid { get; set; }

    }
}