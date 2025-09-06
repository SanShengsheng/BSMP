
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies.Backpack;
using MQKJ.BSMP.ChineseBabies.PropMall;

namespace  MQKJ.BSMP.ChineseBabies.Backpack.Dtos
{
    public class BabyFamilyAssetEditDto:IAddModel<BabyFamilyAsset,Guid>,IEditModel<BabyFamilyAsset,Guid>
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// FamilyId
		/// </summary>
		public int FamilyId { get; set; }



		/// <summary>
		/// Family
		/// </summary>
		public Family Family { get; set; }



		/// <summary>
		/// OwnId
		/// </summary>
		public int? OwnId { get; set; }



		/// <summary>
		/// Own
		/// </summary>
		public Baby Own { get; set; }



		/// <summary>
		/// BabyPropId
		/// </summary>
		public int BabyPropId { get; set; }



		/// <summary>
		/// BabyProp
		/// </summary>
		public BabyProp BabyProp { get; set; }



		/// <summary>
		/// ExpireDateTime
		/// </summary>
		public DateTime? ExpireDateTime { get; set; }



		/// <summary>
		/// IsEquipmenting
		/// </summary>
		public bool IsEquipmenting { get; set; }



		/// <summary>
		/// BabyPropRecordId
		/// </summary>
		public int BabyPropRecordId { get; set; }



		/// <summary>
		/// BabyPropRecord
		/// </summary>
		public BabyPropRecord BabyPropRecord { get; set; }




    }
}