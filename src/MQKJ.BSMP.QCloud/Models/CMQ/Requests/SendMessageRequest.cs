using MQKJ.BSMP.QCloud.Configs;
using MQKJ.BSMP.QCloud.Models.CMQ.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.QCloud.Models.CMQ.Requests
{
    public class SendMessageRequest : CMQRequestBase<SendMessageResponse>
    {
        public SendMessageRequest(QcloudConfig config) : base(config)
        {
          
        }

        [JsonProperty("queueName")]
        public string QueueName { get; set; }
        [JsonProperty("msgBody")]
        public string MsgBody { get; set; }
        [JsonProperty("delaySeconds")]
        public int DelaySeconds { get; set; }

        public override string Action => "SendMessage";
    }
}
