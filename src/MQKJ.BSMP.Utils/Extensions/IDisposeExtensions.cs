using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Utils.Extensions
{
    public static class IDisposeExtensions
    {
        public async static  Task AsDisposeAsync(this IDisposable uowHandler, Func<IDisposable, Task> action)
        {
            using (uowHandler)
            {
                 await action(uowHandler);
            }
        }
        public static void AsDispose(this IDisposable uowHandler, Action<IDisposable> action)
        {
            using (uowHandler)
            {
                action(uowHandler);
            }
        }
        public static T As<T>(this IDisposable dispose) where T: class, IDisposable
        {
            return dispose as T;
        }
    }
}
