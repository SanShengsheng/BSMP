

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;
using System.Collections.Generic;
using MQKJ.BSMP.ChineseBabies.PropMall;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class BabyPropListDto : FullAuditedEntityDto ,ISearchOutModel<BabyProp,int>
    {

        
		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; set; }



		/// <summary>
		/// PropPrices
		/// </summary>
		public ICollection<BabyPropPrice> Prices { get; set; }



		/// <summary>
		/// IsDefault
		/// </summary>
		public bool IsDefault { get; set; }



		/// <summary>
		/// AdditionId
		/// </summary>
		public int? AdditionId { get; set; }



		/// <summary>
		/// Addition
		/// </summary>
		public Reward Addition { get; set; }



		/// <summary>
		/// Level
		/// </summary>
		public PropLevel Level { get; set; }



		/// <summary>
		/// Discount
		/// </summary>
		public double Discount { get; set; }



		/// <summary>
		/// DiscountInfo
		/// </summary>
		public string DiscountInfo { get; set; }



		/// <summary>
		/// IsNewProp
		/// </summary>
		public bool IsNewProp { get; set; }



		/// <summary>
		/// MaxPurchasesNumber
		/// </summary>
		public int MaxPurchasesNumber { get; set; }



		/// <summary>
		/// CoverImg
		/// </summary>
		public string CoverImg { get; set; }



		/// <summary>
		/// BabyPropTypeId
		/// </summary>
		public int? BabyPropTypeId { get; set; }



		/// <summary>
		/// BabyPropType
		/// </summary>
		public BabyPropType BabyPropType { get; set; }



		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; }



		/// <summary>
		/// IsAfterBuyPlayMarquees
		/// </summary>
		public bool IsAfterBuyPlayMarquees { get; set; }



		/// <summary>
		/// PropValue
		/// </summary>
		public decimal PropValue { get; set; }



		/// <summary>
		/// GetWay
		/// </summary>
		public GetWay GetWay { get; set; }



		/// <summary>
		/// Gender
		/// </summary>
		public Gender Gender { get; set; }



		/// <summary>
		/// Code
		/// </summary>
		public int Code { get; set; }



		/// <summary>
		/// PutOn
		/// </summary>
		public EquipmentAbleObject PutOn { get; set; }



		/// <summary>
		/// IsInheritAble
		/// </summary>
		public bool IsInheritAble { get; set; }



		/// <summary>
		/// PurchaseTerms
		/// </summary>
		public ICollection<BabyPropBuyTermType> PurchaseTerms { get; set; }



		/// <summary>
		/// FeatureAdditions
		/// </summary>
		public ICollection<BabyPropFeature> FeatureAdditions { get; set; }




    }
}