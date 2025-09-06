

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;
using System.Collections.Generic;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class BabyEventListDto : FullAuditedEntityDto , ISearchOutModel<BabyEvent, int>
    {

        
		/// <summary>
		/// Type
		/// </summary>
		public IncidentType Type { get; set; }



		/// <summary>
		/// IsBlock
		/// </summary>
		public bool IsBlock { get; set; }



		/// <summary>
		/// OperationType
		/// </summary>
		public OperationType OperationType { get; set; }



		/// <summary>
		/// Aside
		/// </summary>
		public string Aside { get; set; }



		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }



		/// <summary>
		/// CountDown
		/// </summary>
		public int CountDown { get; set; }



		/// <summary>
		/// ConditionType
		/// </summary>
		public ConditionType ConditionType { get; set; }



		/// <summary>
		/// EventId
		/// </summary>
		public int? EventId { get; set; }



		/// <summary>
		/// BabyProperty
		/// </summary>
		public BabyProperty? BabyProperty { get; set; }



		/// <summary>
		/// MaxValue
		/// </summary>
		public int? MaxValue { get; set; }



		/// <summary>
		/// MinValue
		/// </summary>
		public int? MinValue { get; set; }



		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; }



		/// <summary>
		/// ImagePath
		/// </summary>
		public string ImagePath { get; set; }



		/// <summary>
		/// ActivityId
		/// </summary>
		public int? ActivityId { get; set; }



		/// <summary>
		/// RewardId
		/// </summary>
		public int? RewardId { get; set; }



		/// <summary>
		/// ConsumeId
		/// </summary>
		public int? ConsumeId { get; set; }



		/// <summary>
		/// Reward
		/// </summary>
		public Reward Reward { get; set; }



		/// <summary>
		/// Consume
		/// </summary>
		public Reward Consume { get; set; }



		/// <summary>
		/// Options
		/// </summary>

		public IEnumerable<BabyEventOption> Options { get; set; }




    }
}