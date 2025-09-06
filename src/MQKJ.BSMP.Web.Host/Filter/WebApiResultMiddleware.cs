using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Host.Filter
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public class WebApiResultMiddleware: ActionFilterAttribute
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult)
            {
                var objectResult = context.Result as ObjectResult;
                if (objectResult.Value == null)
                {
                    context.Result = new ObjectResult(new { code = 404, sub_msg = "未找到资源", msg = "" });
                }
                else
                {
                    context.Result = new ObjectResult(new { code = 200, msg = "", result = objectResult.Value });
                }
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new ObjectResult(new { code = 404, sub_msg = "未找到资源", msg = "" });
            }
            else if (context.Result is ContentResult)
            {
                context.Result = new ObjectResult(new { code = 200, msg = "", result = (context.Result as ContentResult).Content });
            }
            else if (context.Result is StatusCodeResult)
            {
                context.Result = new ObjectResult(new { code = (context.Result as StatusCodeResult).StatusCode, sub_msg = "", msg = "" });
            }
        }
    }
}
