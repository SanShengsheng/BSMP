
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.PlayerLabels;
using MQKJ.BSMP.Players;

namespace  MQKJ.BSMP.PlayerLabels.Dtos
{
    public class PlayerLabelEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// LabelContent
		/// </summary>
		public string LabelContent { get; set; }



		/// <summary>
		/// PlayerId
		/// </summary>
		public Guid PlayerId { get; set; }



		/// <summary>
		/// Player
		/// </summary>
		//public Player Player { get; set; }




    }
}