
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Friends;
using MQKJ.BSMP.Players;

namespace  MQKJ.BSMP.Friends.Dtos
{
    public class FriendEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// PlayerId
		/// </summary>
		public Guid PlayerId { get; set; }



		/// <summary>
		/// FriendId
		/// </summary>
		public Guid FriendId { get; set; }


        /// <summary>
        /// Floor
        /// </summary>
        public int Floor { get; set; } = 1;



        /// <summary>
        /// HeartCount
        /// </summary>
        //[Range(0, 3)]
        [DefaultValue(3)]
        public int HeartCount { get; set; } = 3;



		/// <summary>
		/// IsUrge
		/// </summary>
		public bool IsUrge { get; set; }




    }
}