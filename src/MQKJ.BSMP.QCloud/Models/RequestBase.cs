using MQKJ.BSMP.QCloud.Configs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MQKJ.BSMP.QCloud.Models
{
    public abstract class RequestBase<TResponse>
        where TResponse : ResponseBase
    {
        protected readonly QcloudConfig _config;
        public RequestBase(QcloudConfig config)
        {
            _config = config;
        }
        [JsonIgnore]
        public abstract string UrlFormat { get; }
        [JsonIgnore]
        public abstract HttpMethod HttpMethod { get; }

        public abstract string GetUrl();

        public abstract string GetBody();
    }
}
