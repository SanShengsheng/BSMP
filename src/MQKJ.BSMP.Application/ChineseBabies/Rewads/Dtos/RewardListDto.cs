

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.BabyPropertyBaseDtos;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class RewardListDto : BabyPropertyBase<int>, ISearchOutModel<Reward,int>
    {

        
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