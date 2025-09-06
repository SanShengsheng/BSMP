using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Abp.Authorization;

namespace MQKJ.BSMP.Web.Host.Startup
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
#pragma warning disable CS0618 // 类型或成员已过时
            var actionAttrs = context.ApiDescription.ActionAttributes();
#pragma warning restore CS0618 // 类型或成员已过时
            if (actionAttrs.OfType<AbpAllowAnonymousAttribute>().Any())
            {
                return;
            }

#pragma warning disable CS0618 // 类型或成员已过时
            var controllerAttrs = context.ApiDescription.ControllerAttributes();
#pragma warning restore CS0618 // 类型或成员已过时
            var actionAbpAuthorizeAttrs = actionAttrs.OfType<AbpAuthorizeAttribute>();

            if (!actionAbpAuthorizeAttrs.Any() && controllerAttrs.OfType<AbpAllowAnonymousAttribute>().Any())
            {
                return;
            }

            var controllerAbpAuthorizeAttrs = controllerAttrs.OfType<AbpAuthorizeAttribute>();
            if (controllerAbpAuthorizeAttrs.Any() || actionAbpAuthorizeAttrs.Any())
            {
                operation.Responses.Add("401", new Response { Description = "Unauthorized" });

                var permissions = controllerAbpAuthorizeAttrs.Union(actionAbpAuthorizeAttrs)
                    .SelectMany(p => p.Permissions)
                    .Distinct();

                if (permissions.Any())
                {
                    operation.Responses.Add("403", new Response { Description = "Forbidden" });
                }

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>
                {
                    new Dictionary<string, IEnumerable<string>>
                    {
                        { "bearerAuth", permissions }
                    }
                };
            }
        }
    }
}
