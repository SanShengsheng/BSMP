
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapTo(typeof(BabyEvent))]
    public class BabyEventEditDto:IAddModel<BabyEvent, int>, IEditModel<BabyEvent, int>
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
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


    }
}