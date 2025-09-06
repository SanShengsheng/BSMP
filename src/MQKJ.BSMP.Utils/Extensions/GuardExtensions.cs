using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Utils.Extensions
{
    public static class GuardExtensions
    {
        public static T CheckNull<T>(this T t, string errorMessage = "对象不能为空")
            where T : class
        {
            if (t == null)
                throw new Exception(errorMessage);

            return t;
        }

        public static T CheckCondition<T>(this T t, bool condition, string errorMessage)
        {
            if (!condition)
                throw new Exception(errorMessage);

            return t;
        }
    }
}
