

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies.PropMall;

namespace MQKJ.BSMP.ChineseBabies.PropMall.Dtos
{
    public class BabyPropBagListDto : FullAuditedEntityDto<Guid> 
    {

        
		/// <summary>
		/// Code
		/// </summary>
		public int Code { get; set; }



		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; }



		/// <summary>
		/// BabyPropId
		/// </summary>
		public int BabyPropId { get; set; }



		/// <summary>
		/// BabyProp
		/// </summary>
		public BabyProp BabyProp { get; set; }



		/// <summary>
		/// Gender
		/// </summary>
		public Gender Gender { get; set; }



		/// <summary>
		/// CurrencyCount
		/// </summary>
		public double CurrencyCount { get; set; }



		/// <summary>
		/// CurrencyType
		/// </summary>
		public CurrencyTypeEnum CurrencyType { get; set; }



		/// <summary>
		/// Order
		/// </summary>
		public int Order { get; set; }




    }
}