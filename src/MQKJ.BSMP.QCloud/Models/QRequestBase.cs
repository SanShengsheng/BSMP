using MQKJ.BSMP.QCloud.Configs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MQKJ.BSMP.QCloud.Models
{
    public abstract class QRequestBase<TResponse> : RequestBase<TResponse>
        where TResponse : ResponseBase
    {
        
        public QRequestBase(QcloudConfig config) : base(config)
        {
          
            SecretId = config.QcloudAppId;
            Nonce = GetNonce();
            Timestamp = GetTimestamp();
        }
        //公共参数
        public abstract string Action { get; }
        [JsonProperty("Region", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Region { get; set; }
        public long Timestamp { get; set; }
        public long Nonce { get; set; }
        public string SecretId { get; set; }
        [JsonIgnore]
        public string Signature { get; set; }
        public string SignatureMethod { get; set; } = "HmacSHA256";
        [JsonIgnore]
        public string Token { get; set; }

        
        public string RequestClient { get; set; } = "SDK_C#_1.0";

        private static Random _random = new Random();
        private static long GetNonce()
        {
            return _random.Next(int.MaxValue);
        }

        private static long GetTimestamp()
        {
            return (int)(((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds);
        }
    }
}
