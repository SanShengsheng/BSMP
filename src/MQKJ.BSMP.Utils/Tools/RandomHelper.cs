using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MQKJ.BSMP.Utils.Tools
{
    public static  class RandomHelper
    {
        public static double[] GenerateRandomCode()
        {
            var result = new StringBuilder();

            var r = new Random(Guid.NewGuid().GetHashCode());

            var A = Convert.ToDouble(r.Next(3, 50));

            var B = Convert.ToDouble(75 - A);

            var C = Convert.ToDouble(100 - A - B);

            var persents = new double[]{ A, B, C };

            return persents;
        }

        /// <summary>
        /// 生成几位数的随机码
        /// </summary>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static string GenerateRandomCode(int length)
        {
            var result = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());

                result.Append(r.Next(0, 10));
            }
            return result.ToString();
        }
        /// <summary>
        /// 生成设置范围内的Double的随机数
        /// eg:_random.NextDouble(1.5, 2.5)
        /// </summary>
        /// <param name="random">Random</param>
        /// <param name="miniDouble">生成随机数的最大值</param>
        /// <param name="maxiDouble">生成随机数的最小值</param>
        /// <param name="round">保留小数位，默认两位</param>
        /// <returns>当Random等于NULL的时候返回0;</returns>
        public static double NextDouble(this Random random, double miniDouble, double maxiDouble,int round=2)
        {
            if (random != null)
            {
                return Math.Round((random.NextDouble() * (maxiDouble - miniDouble) + miniDouble),round);
            }
            else
            {
                return 0.0d;
            }
        }

        public static string GetRandomCode(List<string> codes)
        {
            var code = GenerateRandomCode_V2(6);

            if (codes.Contains(code))
            {
                code = GetRandomCode(codes);
            }

            return code;
        }

        /// <summary>
        /// 判断数组是否有相等的元素
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private static bool IsSameWithHashSet(List<string> arr)
        {
            ISet<string> set = new HashSet<string>();

            for (int i = 0; i < arr.Count; i++)
            {
                set.Add(arr[i]);
            }

            return set.Count != arr.Count;
        }

        /// <summary>
        /// 只接收长度为偶数的数字
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string GenerateRandomCode_V2(int length)
        {
            int tempLength = length / 2;
            byte[] randomBytes = new byte[tempLength];
            RNGCryptoServiceProvider rngServiceProvider = new RNGCryptoServiceProvider();
            rngServiceProvider.GetBytes(randomBytes);
            string result = BitConverter.ToString(randomBytes, 0);

            return result.Replace("-", "");
        }
    }
}
