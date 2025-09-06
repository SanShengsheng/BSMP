
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapTo(typeof(ChangeProfessionCost))]
    public class ChangeProfessionCostEditDto:IAddModel<ChangeProfessionCost,int>,IEditModel<ChangeProfessionCost,int>
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
		/// <summary>
		/// ProfessionId
		/// </summary>
		public int ProfessionId { get; set; }



		/// <summary>
		/// Profession
		/// </summary>
		public Profession Profession { get; set; }



		/// <summary>
		/// CostType
		/// </summary>
		public CostType CostType { get; set; }



		/// <summary>
		/// Cost
		/// </summary>
		public int Cost { get; set; }




    }
}