using System;
using System.Net.Http;
using System.Threading.Tasks;
using JCSoft.Core.HttpClient;
using MQKJ.BSMP.QCloud.Models;
using Newtonsoft.Json;

namespace MQKJ.BSMP.QCloud
{
    public class QCloudApiClient : IQCloudApiClient
    {
        private readonly IHttpHelper _helper;
        public QCloudApiClient(IHttpHelper helper)
        {
            _helper = helper;
        }

        public async Task<TResponse> Execute<TRequest, TResponse>(TRequest request)
            where TRequest : RequestBase<TResponse>
            where TResponse : ResponseBase
        {
            return request.HttpMethod == HttpMethod.Get ?
                await _helper.GetAsync<TResponse>(request.GetUrl()) :
                await _helper.PostAsync<TResponse>(request.GetUrl(), request.GetBody());
        }
    }
}
