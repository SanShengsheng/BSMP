using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.HttpContextHelper
{
    public static class CustomHttpContext
    {
        public static IServiceProvider serviceProvider;

        static CustomHttpContext() { }

        public static HttpContext Current
        {
            get
            {
                object factory = serviceProvider.GetService(typeof(Microsoft.AspNetCore.Http.IHttpContextAccessor));

                HttpContext context = ((HttpContextAccessor)factory).HttpContext;

                return context;
            }
        }
    }
}
