

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ActiveApply;
using MQKJ.BSMP.CommonEnum;

namespace MQKJ.BSMP.ActiveApply.Dtos
{
    public class RiskActiveApplyListDto : FullAuditedEntityDto<Guid> 
    {

        
		/// <summary>
		/// Season
		/// </summary>
		[Required(ErrorMessage="Season不能为空")]
		public int Season { get; set; }



		/// <summary>
		/// NickName
		/// </summary>
		[Required(ErrorMessage="NickName不能为空")]
		public string NickName { get; set; }



		/// <summary>
		/// Gender
		/// </summary>
		public EGender Gender { get; set; }



		/// <summary>
		/// BirthDateTime
		/// </summary>
		public DateTime BirthDateTime { get; set; }



		/// <summary>
		/// Height
		/// </summary>
		public int Height { get; set; }



		/// <summary>
		/// Address
		/// </summary>
		public string Address { get; set; }



		/// <summary>
		/// Hobbies
		/// </summary>
		public string Hobbies { get; set; }



		/// <summary>
		/// SelfIntroduction
		/// </summary>
		public string SelfIntroduction { get; set; }



		/// <summary>
		/// DeclarationOfDating
		/// </summary>
		public string DeclarationOfDating { get; set; }



		/// <summary>
		/// Source
		/// </summary>
		public string Source { get; set; }


        public string Code { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string WechatId
        {
            get; set;
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string TeleNumber { get; set; }
    }
}