using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.MultiTenancy;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using MQKJ.BSMP.Resolve;
using System.Linq;

namespace MQKJ.BSMP.Resolves
{
    public class HttpHeaderTenantResolveContributor : ITenantResolveContributor, ITransientDependency
    {
        public ILogger Logger { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpHeaderTenantResolveContributor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            Logger = NullLogger.Instance;
        }

        public int? ResolveTenantId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return null;
            }

            var tenantIdHeader = httpContext.Request.Headers[MultiTenancyConstsBSMP.TenantIdResolveKey];
            if (tenantIdHeader == string.Empty || tenantIdHeader.Count < 1)
            {
                return null;
            }

            if (tenantIdHeader.Count > 1)
            {
                Logger.Warn(
                    $"HTTP request includes more than one {MultiTenancyConstsBSMP.TenantIdResolveKey} header value. First one will be used. All of them: {tenantIdHeader.JoinAsString(", ")}"
                    );
            }

            int tenantId;
            return !int.TryParse(tenantIdHeader.First(), out tenantId) ? (int?)null : tenantId;
        }
    }
}
