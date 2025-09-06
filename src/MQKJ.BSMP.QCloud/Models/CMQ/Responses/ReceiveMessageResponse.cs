using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.QCloud.Models.CMQ.Responses
{
    public class ReceiveMessageResponse : CMQResponseBase
    {
        public string RequestId { get; set; }
        public string MsgBody { get; set; }
        public string MsgId { get; set; }
        public string ReceiptHandle { get; set; }
        public int EnqueueTime { get; set; }
        public int FirstDequeueTime { get; set; }
        public int NextVisibleTime { get; set; }
        public int DequeueCount { get; set; }
    }
}
