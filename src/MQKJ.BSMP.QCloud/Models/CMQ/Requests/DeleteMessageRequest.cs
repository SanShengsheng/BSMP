using MQKJ.BSMP.QCloud.Configs;
using MQKJ.BSMP.QCloud.Models.CMQ.Responses;
using Newtonsoft.Json;

namespace MQKJ.BSMP.QCloud.Models.CMQ.Requests
{
    public class DeleteMessageRequest : CMQRequestBase<DeleteMessageResponse>
    {
        public DeleteMessageRequest(QcloudConfig config) : base(config)
        {
        }

        public override string Action => "DeleteMessage";

        [JsonProperty("queueName")]
        public string QueueName { get; set; }
        /// <summary>
        /// 上次消费返回唯一的消息句柄，用于删除消息。
        /// </summary>
        [JsonProperty("receiptHandle")]
        public string ReceiptHandle { get; set; }
    }
}
