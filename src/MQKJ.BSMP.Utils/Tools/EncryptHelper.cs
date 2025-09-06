using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MQKJ.BSMP.Utils.Tools
{
    public class EncryptHelper
    {
        public static string EncryptWithMD5(string source)
        {
            byte[] sor = Encoding.UTF8.GetBytes(source);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(sor);
            StringBuilder strBuilder = new StringBuilder(40);
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));//加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位

            }
            return strBuilder.ToString();
        }

        /// <summary>
		/// 获取大写的MD5签名结果
		/// </summary>
		/// <param name="encypStr"></param>
		/// <param name="charset"></param>
		/// <returns></returns>
		public static string GetMD5(string encypStr, string charset)
        {
            string retStr;
#if NET45
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();
#else
            var m5 = MD5.Create();
#endif

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception)
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }
    }
}
