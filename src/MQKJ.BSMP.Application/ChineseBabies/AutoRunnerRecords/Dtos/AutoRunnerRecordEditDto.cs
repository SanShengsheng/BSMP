
using System;
using Abp.AutoMapper;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapTo(typeof(AutoRunnerRecord))]
    public class AutoRunnerRecordEditDto : IAddModel<AutoRunnerRecord, Guid>, IEditModel<AutoRunnerRecord, Guid>
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
		/// PlayerId
		/// </summary>
		public Guid PlayerId { get; set; }



		/// <summary>
		/// GroupId
		/// </summary>
		public int GroupId { get; set; }



		/// <summary>
		/// ActionType
		/// </summary>
		public ActionType ActionType { get; set; }



		/// <summary>
		/// RelateionId
		/// </summary>
		public string RelateionId { get; set; }



		/// <summary>
		/// OriginalData
		/// </summary>
		public string OriginalData { get; set; }



		/// <summary>
		/// NewData
		/// </summary>
		public string NewData { get; set; }



		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; }




    }
}