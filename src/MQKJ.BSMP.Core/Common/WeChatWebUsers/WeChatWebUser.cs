using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

namespace MQKJ.BSMP.Common
{
    [Table("WeChatWebUsers")]
    public class WeChatWebUser:FullAuditedEntity<Guid>
    {
        public string NickName { get; set; }

        public string HeadUrl { get; set; }

        public string UnionId { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        /// <summary>
        /// 1-男性2-女性
        /// </summary>
        public int Gender { get; set; }

        public string Province { get; set; }

        private string _city;
        public string City
        {
            get
            {
                return _city;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _city = Regex.Replace(value.ToString(), @"[^\u4e00-\u9fa5]","");
                }
            }
        }

        public string UserName { get; set; }

        public string Profession { get; set; }

        /// <summary>
        /// 兴趣
        /// </summary>
        public string Interest { get; set; }

        /// <summary>
        /// access_token过期时间
        /// </summary>
        public DateTime AccessTokenExpireTime { get; set; }

        /// <summary>
        /// Refresh_token过期时间
        /// </summary>
        public DateTime RefreshTokenExpireTime { get; set; }

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

    public enum UserState
    {
        [EnumHelper.EnumDescription("未知")]
        UnKnown = 0,

        [EnumHelper.EnumDescription("已提交资料")]
        SumitInfo = 1,

        [EnumHelper.EnumDescription("已报名")]
        Enrolment = 2,

        [EnumHelper.EnumDescription("已匹配")]
        AlreadyMatched = 3,

    }
}
