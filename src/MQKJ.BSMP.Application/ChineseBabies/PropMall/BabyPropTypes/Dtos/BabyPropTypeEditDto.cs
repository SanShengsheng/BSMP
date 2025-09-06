
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    public class BabyPropTypeEditDto:IAddModel<BabyPropType, int>,IEditModel<BabyPropType, int>
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
		/// Img
		/// </summary>
		public string Img { get; set; }



		/// <summary>
		/// Code
		/// </summary>
		public int Code { get; set; }



		/// <summary>
		/// Sort
		/// </summary>
		public int Sort { get; set; }




    }
}