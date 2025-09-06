
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.PropMall;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    public class BabyPropEditDto:IAddModel<BabyProp,int>,IEditModel<BabyProp,int>
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; set; }



		/// <summary>
		/// PropPrices
		/// </summary>
		public ICollection<BabyPropPrice> BabyPropPrices { get; set; }



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
        /// 条件集合
        /// </summary>
        public virtual ICollection<BabyPropTerm> BabyPropTerms { get; set; }

        /// <summary>
        /// 功能集合
        /// </summary>
        public virtual ICollection<BabyPropFeature> BabyPropFeatures { get; set; }




    }
}