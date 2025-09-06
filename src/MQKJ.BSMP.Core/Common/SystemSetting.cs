using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP
{
   public class SystemSetting : FullAuditedEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

        public string GroupName { get; set; }

        public int Code { get; set; }

    }
    public enum SystemSettingCode
    {

    }
}
