using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.EnterprisePayments.Dtos
{
    public class WithDrawMoneyForAgentInput
    {
        public Guid Id { get; set; }

        public bool IsTest { get; set; }

        public string IpStr { get; set; }
    }
}
