using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using Abp.Extensions;
using MQKJ.BSMP.Common.Companies;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Questions;
using Newtonsoft.Json;

namespace MQKJ.BSMP.Authorization.Users
{
    public class User : AbpUser<User>
    {
        [ForeignKey("CompanyId")]
        public virtual Company UserCompany { get; set; }
        public int? CompanyId { get; set; }
        public const string DefaultPassword = "123qwe";

        //curstom code 
        /// <summary>
        /// 问题创建人
        /// </summary>
        [JsonIgnore]
        public ICollection<Question> QuestionCreators { get; set; }
        /// <summary>
        /// 问题审核人
        /// </summary>
        [JsonIgnore]//为了避免调用接口时循环引用的问题
        public ICollection<Question> QuestionAuditors { get; set; }
        /// <summary>
        /// 问题校验人（上线）
        /// </summary>
        [JsonIgnore]
        public ICollection<Question> QuestionCheckOnes { get; set; }

        //curstom code


        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}
