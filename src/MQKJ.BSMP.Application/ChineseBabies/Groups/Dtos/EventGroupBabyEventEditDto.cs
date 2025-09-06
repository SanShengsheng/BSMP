
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapTo(typeof(EventGroupBabyEvent))]
    public class EventGroupBabyEventEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
		/// <summary>
		/// GroupId
		/// </summary>
		public int GroupId { get; set; }



		/// <summary>
		/// EventId
		/// </summary>
		public int EventId { get; set; }




    }
}