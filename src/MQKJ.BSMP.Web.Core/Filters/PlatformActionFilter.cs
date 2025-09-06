using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MQKJ.BSMP.Filters
{
    /// <summary>
    /// 禁止重复提交
    /// </summary>
    public class ForbidRepeatActionFilter : Attribute, IActionFilter
    {
        private static MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        public const string hiddenToken = "hiddenToken";
        //private ILog _log;

        public ForbidRepeatActionFilter()
        {
            //this._log = LogManager.GetLogger(IStartup.Repository.Name, typeof(PlatformActionFilter));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
        /// <summary>
        /// action 执行之前
        /// </summary>
        /// <param name="context"></param>
        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string httpMethod = WebUtility.HtmlEncode(filterContext.HttpContext.Request.Method);
            if (httpMethod == "POST")
            {
                //使用请求路径作为唯一key
                string path = filterContext.HttpContext.Request.Path;
                string cacheToken = $"{hiddenToken}_{path}";
                string keyValue = new Guid().ToString() + DateTime.Now.Ticks;

                if (path != null)
                {
                    //var cache = iZen.Utils.Core.iCache.CacheManager.GetCacheValue(cacheToken);
                    var cv = cache.Get(cacheToken);
                    if (cv == null)
                    {
                        //iZen.Utils.Core.iCache.CacheManager.SetChacheValueSeconds(cacheToken, keyValue, 1);
                        //设置缓存1秒过期
                        cache.Set(cacheToken, keyValue, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromSeconds(1) });
                        //_log.Info($"提交成功");
                    }
                    else
                    {
                        //_log.Error($"{filterContext.HttpContext.Request.Method},请不要重复提交");
                        //设置了 filterContext.Result 表示返回过滤失败的结果
                        //filterContext.Result = new BadRequestObjectResult(filterContext.ModelState);
                        filterContext.Result = new BadRequestObjectResult("请不要重复提交");
                    }

                }
                return;
            }
            this.OnActionExecuting(filterContext);
        }

    }
}
