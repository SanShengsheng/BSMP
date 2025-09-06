
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Players;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    public class BabyEventRecordEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// FamilyId
		/// </summary>
		public int FamilyId { get; set; }



		/// <summary>
		/// PlayerId
		/// </summary>
		public Guid PlayerId { get; set; }



		/// <summary>
		/// EventId
		/// </summary>
		public int EventId { get; set; }



		/// <summary>
		/// Event
		/// </summary>
		public BabyEvent Event { get; set; }



		/// <summary>
		/// State
		/// </summary>
		public EventRecordState State { get; set; }



		/// <summary>
		/// StartTime
		/// </summary>
		public DateTime? StartTime { get; set; }



		/// <summary>
		/// EndTime
		/// </summary>
		public DateTime? EndTime { get; set; }



		/// <summary>
		/// OptionId
		/// </summary>
		public int OptionId { get; set; }



		/// <summary>
		/// Option
		/// </summary>
		public BabyEventOption Option { get; set; }



		/// <summary>
		/// Player
		/// </summary>
		public Player Player { get; set; }



		/// <summary>
		/// Family
		/// </summary>
		public Family Family { get; set; }




    }
}