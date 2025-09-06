
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Common;

namespace  MQKJ.BSMP.Common.Dtos
{
    public class WeChatWebUserEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
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



		/// <summary>
		/// Province
		/// </summary>
		public string Province { get; set; }



		/// <summary>
		/// City
		/// </summary>
		public string City { get; set; }



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




    }
}