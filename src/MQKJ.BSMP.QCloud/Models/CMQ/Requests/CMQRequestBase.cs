using MQKJ.BSMP.QCloud.Configs;
using MQKJ.BSMP.QCloud.Helps;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace MQKJ.BSMP.QCloud.Models.CMQ.Requests
{
    public abstract class CMQRequestBase<TResponse> : QRequestBase<TResponse>
        where TResponse : ResponseBase
    {
        private readonly QcloudConfig _qcloundConfig;
        public CMQRequestBase(QcloudConfig config) : base(config)
        {
            _qcloundConfig = config;
        }
        
        [JsonIgnore]
        public virtual string Domain => _config.IsPrivate ?
            $"cmq-queue-{_config.CmqConfig?.Region}.api.tencentyun.com" :
            $"cmq-queue-{_config.CmqConfig?.Region}.api.qcloud.com";

        public override string UrlFormat => _config.IsPrivate ? 
            $"http://{this.Domain}" : 
            $"https://{this.Domain}";
        public override string GetUrl()
        {
            return $"{UrlFormat.TrimEnd('/')}/v2/index.php?{BuildParamStr()}";
        }

        protected virtual string BuildParamStr()
        {
            var json = JsonConvert.SerializeObject(this);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            var sortDict = new SortedDictionary<string, object>(dict, StringComparer.Ordinal);

            var paramstr = String.Join("&", sortDict.Select(s => $"{s.Key.Replace("_", ".")}={s.Value}"));
            var signStr = $"{this.HttpMethod}{Domain.TrimEnd('/')}/v2/index.php?{paramstr}";
            var sign = Sign.Signature(signStr, _config.QcouldAppSecret, this.SignatureMethod);
            dict.Add("Signature", sign);

            var result = String.Join("&", dict.Select(s => $"{s.Key}={HttpUtility.UrlEncode(s.Value?.ToString())}"));

            return result;
        }

        public override HttpMethod HttpMethod => HttpMethod.Get;

        public override string GetBody() => String.Empty;
    }
}
