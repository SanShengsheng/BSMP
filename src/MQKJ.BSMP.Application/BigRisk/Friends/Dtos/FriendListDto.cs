

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Friends;
using MQKJ.BSMP.Players;
using Abp.AutoMapper;
using Newtonsoft.Json;

namespace MQKJ.BSMP.Friends.Dtos
{
    public class FriendListDto : EntityDto<Guid> 
    {

        
		/// <summary>
		/// PlayerId
		/// </summary>
		public Guid PlayerId { get; set; }



		/// <summary>
		/// FriendId
		/// </summary>
		public Guid FriendId { get; set; }



        /// <summary>
        /// MyFriend
        /// </summary>
        public MyFriend MyFriend { get; set; }

        [JsonIgnore]
        public DateTime CreationTime { get; set; }

        [JsonIgnore]
        public DateTime LastModificationTime { get; set; }



        /// <summary>
        /// Floor
        /// </summary>
        public int Floor { get; set; }



        /// <summary>
        /// HeartCount
        /// </summary>
        //public int HeartCount { get; set; }



		/// <summary>
		/// IsUrge
		/// </summary>
		public bool IsUrge { get; set; }


    }

    [AutoMapFrom(typeof(Player))]
    public class MyFriend
    {
        public string NickName { get; set; }


        public Guid Id { get; set; }


        public string HeadUrl { get; set; }
    }
}