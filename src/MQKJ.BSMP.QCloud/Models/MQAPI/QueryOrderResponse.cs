using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.QCloud.Models.MQAPI
{
    public class QueryOrderResponse : MQApiResponseModel<QueryOrderResult>
    {
    }

    public class QueryOrderResult
    {
        public bool IsSuccess { get; set; }
    }
}
