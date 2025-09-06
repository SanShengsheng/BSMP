using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MQKJ.BSMP.QCloud.Helps
{
    /// <summary>
    /// 签名类
    /// </summary>
    public class Sign
    {
        ///<summary>生成签名</summary>
        ///<param name="signStr">被加密串</param>
        ///<param name="secret">加密密钥</param>
        ///<returns>签名</returns>
        public static string Signature(string signStr, string secret, string SignatureMethod)
        {
            if (SignatureMethod.ToUpper() == "HMACSHA256")
                using (HMACSHA256 mac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
                {
                    byte[] hash = mac.ComputeHash(Encoding.UTF8.GetBytes(signStr));
                    return Convert.ToBase64String(hash);
                }
            else
                using (HMACSHA1 mac = new HMACSHA1(Encoding.UTF8.GetBytes(secret)))
                {
                    byte[] hash = mac.ComputeHash(Encoding.UTF8.GetBytes(signStr));
                    return Convert.ToBase64String(hash);
                }
        }

      
    }
}
