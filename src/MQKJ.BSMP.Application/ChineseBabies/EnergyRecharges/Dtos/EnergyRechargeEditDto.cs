
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    public class EnergyRechargeEditDto:IAddModel<EnergyRecharge,int>,IEditModel<EnergyRecharge,int>
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
		/// <summary>
		/// EnergyCount
		/// </summary>
		public int EnergyCount { get; set; }



		/// <summary>
		/// CointCount
		/// </summary>
		public int CointCount { get; set; }




    }
}