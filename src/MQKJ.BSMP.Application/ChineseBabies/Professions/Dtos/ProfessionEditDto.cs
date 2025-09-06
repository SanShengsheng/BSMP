
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    public class ProfessionEditDto:IAddModel<Profession,int>,IEditModel<Profession,int>
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }



		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; }



		/// <summary>
		/// Grade
		/// </summary>
		public int Grade { get; set; }



		/// <summary>
		/// Gender
		/// </summary>
		public int Gender { get; set; }



		/// <summary>
		/// Salary
		/// </summary>
		public double Salary { get; set; }



		/// <summary>
		/// ImagePath
		/// </summary>
		public string ImagePath { get; set; }



		/// <summary>
		/// RewardId
		/// </summary>
		public int? RewardId { get; set; }



		/// <summary>
		/// Costs
		/// </summary>
		public IEnumerable<ChangeProfessionCost> Costs { get; set; }




    }
}