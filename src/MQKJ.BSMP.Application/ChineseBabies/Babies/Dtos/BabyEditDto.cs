
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    public class BabyEditDto  : BabyPropertyBase<int>,IAddModel<Baby,int>,IEditModel<Baby,int>
    {

        /// <summary>
        /// Id
        /// </summary>
        public new int? Id { get; set; }         


        
		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }



		/// <summary>
		/// Gender
		/// </summary>
		public Gender Gender { get; set; }



		/// <summary>
		/// FamilyId
		/// </summary>
		public int FamilyId { get; set; }



		/// <summary>
		/// CoverImage
		/// </summary>
		public string CoverImage { get; set; }



		/// <summary>
		/// State
		/// </summary>
		public BabyState State { get; set; }



		/// <summary>
		/// BabyEndingId
		/// </summary>
		public int? BabyEndingId { get; set; }



		/// <summary>
		/// BabyEnding
		/// </summary>
		public BabyEnding BabyEnding { get; set; }

        /// <summary>
        /// 当前阶段ID
        /// </summary>
        public int? GroupId { get; set; }
        public EventGroup Group { get; set; }
        /// <summary>
        /// 出生身长
        /// </summary>
        public double BirthLength { get; set; }
        /// <summary>
        /// 出生体重
        /// </summary>
        public double BirthWeight { get; set; }
        /// <summary>
        /// 出生医院
        /// </summary>
        public string BirthHospital { get; set; }
        /// <summary>
        /// 成长潜力
        /// </summary>
        public int Potential { get; set; }
        /// <summary>
        /// 成长总值
        /// </summary>
        public double GrowthTotal { get; set; }
        /// <summary>
        /// 第几胎
        /// </summary>
        public int BirthOrder { get; set; }
        /// <summary>
        /// 爸爸是否查看宝宝出生动画
        /// </summary>
        public bool IsWatchBirthMovieFather { get; set; }
        /// <summary>
        /// 妈妈是否查看宝宝出生动画
        /// </summary>
        public bool IsLoadBirthMovieMother { get; set; }
    }
}