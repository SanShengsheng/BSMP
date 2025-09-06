

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class EnergyRechargeListDto : ISearchOutModel<EnergyRecharge,int>
    {

        public int Id { get; set; }

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