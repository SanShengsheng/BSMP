
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    public class RewardEditDto:IAddModel<Reward,int>, IEditModel<Reward, int>
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

        public int CoinCount { get; set; }



        /// <summary>
        /// Type
        /// </summary>
        public RewardType Type { get; set; }




    }
}