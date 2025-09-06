
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.StaminaRecords;

namespace  MQKJ.BSMP.StaminaRecords.Dtos
{
    [AutoMapTo(typeof(StaminaRecord))]
    public class StaminaRecordEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// StaminaCount
		/// </summary>
		public int StaminaCount { get; set; }



		/// <summary>
		/// PlayerId
		/// </summary>
		public Guid PlayerId { get; set; }



		/// <summary>
		/// Player
		/// </summary>
		public Player Player { get; set; }

    }
}