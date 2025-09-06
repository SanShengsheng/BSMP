

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;
using Newtonsoft.Json;
using Abp.AutoMapper;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapFrom(typeof(ChangeProfessionCost))]
    public class ChangeProfessionCostListDto : ISearchOutModel<ChangeProfessionCost,int>
    {
        public int Id { get; set; }

        /// <summary>
        /// ProfessionId
        /// </summary>
        [JsonIgnore]
        public int ProfessionId { get; set; }



        /// <summary>
        /// Profession
        /// </summary>
        [JsonIgnore]
        public ProfessionListDto Profession { get; set; }



		/// <summary>
		/// CostType
		/// </summary>
		public CostType CostType { get; set; }



		/// <summary>
		/// Cost
		/// </summary>
		public decimal Cost { get; set; }




    }
}