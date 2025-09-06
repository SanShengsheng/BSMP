
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapTo(typeof(Information))]
    public class InformationEditDto : IAddModel<Information, Guid>, IEditModel<Information, Guid>
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; set; }



		/// <summary>
		/// SenderId
		/// </summary>
		public Guid? SenderId { get; set; }



		/// <summary>
		/// ReceiverId
		/// </summary>
		public Guid? ReceiverId { get; set; }



		/// <summary>
		/// FamilyId
		/// </summary>
		public int? FamilyId { get; set; }



		/// <summary>
		/// Type
		/// </summary>
		public InformationType Type { get; set; }

        public InformationState State { get; set; } = InformationState.Create;

        public SystemInformationType SystemInformationType { get; set; }

        public string Remark { get; set; }
        public NoticeType NoticeType { get; set; }

    }
}