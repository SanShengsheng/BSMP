using MQKJ.BSMP.QCloud.Models;
using System.Threading.Tasks;

namespace MQKJ.BSMP.QCloud
{
    public interface IQCloudApiClient
    {
        Task<TResponse> Execute<TRequest, TResponse>(TRequest request)
            where TResponse:ResponseBase
            where TRequest:RequestBase<TResponse>;
    }
}
