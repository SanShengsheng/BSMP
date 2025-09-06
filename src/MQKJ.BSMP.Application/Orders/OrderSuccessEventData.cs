using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Orders
{
    public class OrderSuccessEventData : EventData
    {
        public Order Order { get; set; }
    }
}
