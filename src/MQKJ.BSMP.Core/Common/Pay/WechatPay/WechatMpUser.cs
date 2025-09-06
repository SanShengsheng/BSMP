using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.WechatPay
{
    public class WechatMpUser : FullAuditedEntity<Guid>
    {
        public string OpenId { get; set; }

        public string NickName { get; set; }

        /// <summary>
        /// 1-男2-女
        /// </summary>
        public virtual int Gender { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string Country { get; set; }


        public virtual string HeadUrl { get; set; }

        public string UnionId { get; set; }
    }
}
