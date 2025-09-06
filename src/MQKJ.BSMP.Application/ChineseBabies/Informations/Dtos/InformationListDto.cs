

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Players;
using Abp.AutoMapper;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class InformationListDto :ISearchOutModel<Information, Guid>
    {
        public Guid Id { get; set; }
        
		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; set; }

        public DateTime CreationTime { get; set; }



		/// <summary>
		/// SenderId
		/// </summary>
		//public Guid? SenderId { get; set; }



		/// <summary>
		/// ReceiverId
		/// </summary>
		//public Guid ReceiverId { get; set; }



		/// <summary>
		/// FamilyId
		/// </summary>
		public int? FamilyId { get; set; }



		/// <summary>
		/// Type
		/// </summary>
		public InformationType Type { get; set; }



		/// <summary>
		/// Sender
		/// </summary>
		public InformationPlayerDto Sender { get; set; }



		/// <summary>
		/// Receiver
		/// </summary>
		public InformationPlayerDto Receiver { get; set; }

    }

    [AutoMapFrom(typeof(Player))]
    public class InformationPlayerDto
    {
        public Guid Id { get; set; }

        public string NickName { get; set; }

        public string HeadUrl { get; set; }
    }
}