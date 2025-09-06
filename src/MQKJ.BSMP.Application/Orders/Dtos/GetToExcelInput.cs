using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Orders.Dtos
{
    public class GetToExcelInput
    {
        public string NickName { get; set; }

        public OrderState OrderState { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
