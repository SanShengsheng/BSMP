using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using MQKJ.BSMP.QCloud.Configs;

namespace MQKJ.BSMP.QCloud.Models.MQAPI
{
    public class QueryOrderReqeust : RequestBase<QueryOrderResponse>
    {
        public QueryOrderReqeust(QcloudConfig config) : base(config)
        {
        }

        //public override string UrlFormat => $"{_config.MqApiUrl}/api/baby/CoinRecharge/queryorder";
        public override string UrlFormat => $"{_config.MqApiUrl}/api/baby/CoinRecharge/queryorder";

        public override HttpMethod HttpMethod => HttpMethod.Get;

        public override string GetBody()
        {
            return String.Empty;
        }

        public override string GetUrl()
        {
            return $"{UrlFormat}?outTradeNo={OutTradeNo}";
        }

        public string OutTradeNo { get; set; }
    }
}
