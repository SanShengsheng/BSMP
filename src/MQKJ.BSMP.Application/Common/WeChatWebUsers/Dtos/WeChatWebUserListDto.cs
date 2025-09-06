

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Common;
using Abp.AutoMapper;
using System.Text.RegularExpressions;

namespace MQKJ.BSMP.Common.Dtos
{
    [AutoMapFrom(typeof(WeChatWebUser))]
    public class WeChatWebUserListDto
    {
        public Guid Id { get; set; }

        
		/// <summary>
		/// NickName
		/// </summary>
		public string NickName { get; set; }



		/// <summary>
		/// HeadUrl
		/// </summary>
		public string HeadUrl { get; set; }



		/// <summary>
		/// UnionId
		/// </summary>
		public string UnionId { get; set; }



		/// <summary>
		/// Age
		/// </summary>
		public int Age { get; set; }



		/// <summary>
		/// PhoneNumber
		/// </summary>
		public string PhoneNumber { get; set; }



		/// <summary>
		/// Gender
		/// </summary>
		public int Gender { get; set; }



        public string Profession { get; set; }

        /// <summary>
        /// 兴趣
        /// </summary>
        public string Interest { get; set; }

        public string UserName { get; set; }

        public DateTime CreationTime { get; set; }



        /// <summary>
        /// City
        /// </summary>
        public string City
        {
            get;
            set;
        }



		/// <summary>
		/// Country
		/// </summary>
		public string Country { get; set; }



		/// <summary>
		/// AccessTokenExpireTime
		/// </summary>
		public DateTime AccessTokenExpireTime { get; set; }



		/// <summary>
		/// RefreshTokenExpireTime
		/// </summary>
		public DateTime RefreshTokenExpireTime { get; set; }



		/// <summary>
		/// OpenId
		/// </summary>
		public string OpenId { get; set; }

        public UserState State { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string WechatAccount { get; set; }

        /// <summary>
        /// 对方的微信号
        /// </summary>
        public string OtherWechatAccount { get; set; }

    }
}