
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapTo(typeof(AutoRunnerConfig))]
    public class AutoRunnerConfigEditDto : IAddModel<AutoRunnerConfig, Int32>, IEditModel<AutoRunnerConfig, Int32>
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
		/// <summary>
		/// FamilyId
		/// </summary>
		public int FamilyId { get; set; }



		/// <summary>
		/// PlayerId
		/// </summary>
		public Guid PlayerId { get; set; }



		/// <summary>
		/// GroupId
		/// </summary>
		public int GroupId { get; set; }



		/// <summary>
		/// ProfressionId
		/// </summary>
		public int? ProfressionId { get; set; }


        /// <summary>
        /// 成长事件消费
        /// </summary>
        public ConsumeLevel ConsumeLevel { get; set; }
        /// <summary>
        /// 学习事件消耗
        /// </summary>
        public ConsumeLevel StudyLevel { get; set; }
        /// <summary>
        /// 精力购买次数
        /// </summary>
        public int BuyCount { get; set; }
        public int StudyCount { get; set; }
        public LevelAction LevelAction { get; set; }


    }
}