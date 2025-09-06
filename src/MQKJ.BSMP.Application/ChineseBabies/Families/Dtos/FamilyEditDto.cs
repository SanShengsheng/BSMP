
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Players;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapTo(typeof(Family))]
    public class FamilyEditDto:  IAddModel<Family, int>, IEditModel<Family, int>
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
		/// <summary>
		/// FatherId
		/// </summary>
		public Guid FatherId { get; set; }



		/// <summary>
		/// MotherId
		/// </summary>
		public Guid MotherId { get; set; }



		/// <summary>
		/// Deposit
		/// </summary>
		public double Deposit { get; set; }



		/// <summary>
		/// Happiness
		/// </summary>
		public double Happiness { get; set; }



		/// <summary>
		/// Type
		/// </summary>
		public int Type { get; set; }



        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }



		/// <summary>
		/// ChargeAmount
		/// </summary>
		public double ChargeAmount { get; set; }

        public int FightCount { get; set; }


    }

}