
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Players;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapTo(typeof(PlayerProfession))]
    public class PlayerProfessionEditDto:IAddModel<PlayerProfession,int>, IEditModel<PlayerProfession, int>
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
		/// <summary>
		/// PlayerId
		/// </summary>
		public Guid PlayerId { get; set; }



		/// <summary>
		/// FamilyId
		/// </summary>
		public int FamilyId { get; set; }



		/// <summary>
		/// ProfessionId
		/// </summary>
		public int ProfessionId { get; set; }



		/// <summary>
		/// Profession
		/// </summary>
		public Profession Profession { get; set; }



		/// <summary>
		/// Family
		/// </summary>
		public Family Family { get; set; }



		/// <summary>
		/// Player
		/// </summary>
		public Player Player { get; set; }




    }
}