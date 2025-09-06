using MQKJ.BSMP.QCloud.Configs;
using MQKJ.BSMP.QCloud.Models.CMQ.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.QCloud.Models.CMQ.Requests
{
    public class ReceiveMessageRequest : CMQRequestBase<ReceiveMessageResponse>
    {
        public ReceiveMessageRequest(QcloudConfig config) : base(config)
        {
        }

        [JsonProperty("queueName")]
        public string QueueName { get; set; }
        [JsonProperty("pollingWaitSeconds")]
        public int PollingWaitSeconds { get; set; } = 30;

        public override string Action => "ReceiveMessage";
    }
}
