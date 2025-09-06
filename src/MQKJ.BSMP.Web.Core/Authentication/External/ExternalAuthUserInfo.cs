using System;

namespace MQKJ.BSMP.Authentication.External
{
    public class ExternalAuthUserInfo
    {
        public string ProviderKey { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string Surname { get; set; }

        public string Provider { get; set; }

        /// <summary>
        /// 微信用
        /// </summary>
        public string HeadUrl { get; set; }



        public Guid PlayerId { get; set; }

        public int? AgentId { get; set; }
    }
}
