using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MQKJ.BSMP.Utils.Crypt
{
    /// <summary>
    ///  加密 sha256 HMAC
    /// </summary>
    public static class SHA256Crypt
    {
        /// <summary>
        /// 加密 sha256
        /// </summary>
        /// <param name="strIN"></param>
        /// <returns></returns>
        public static string Sha256(string strIN)
        {
            //string strIN = getstrIN(strIN); 
            byte[] tmpByte;
            SHA256 sha256 = new SHA256Managed();
            tmpByte = sha256.ComputeHash(GetKeyByteArray(strIN));
            StringBuilder rst = new StringBuilder();
            for (int i = 0; i < tmpByte.Length; i++) {
                rst.Append(tmpByte[i].ToString("x2"));
            }
            sha256.Clear();
            return rst.ToString();
            //return GetStringValue(tmpByte); }
        }

        private static string GetStringValue(byte[] Byte) { string tmpString = ""; UTF8Encoding Asc = new UTF8Encoding(); tmpString = Asc.GetString(Byte); return tmpString; }


        private static byte[] GetKeyByteArray(string strKey) { UTF8Encoding Asc = new UTF8Encoding(); int tmpStrLen = strKey.Length; byte[] tmpByte = new byte[tmpStrLen - 1]; tmpByte = Asc.GetBytes(strKey); return tmpByte; }

        public static string HmacSHA256(string message, string secret) { secret = secret ?? ""; var encoding = new System.Text.UTF8Encoding(); byte[] keyByte = encoding.GetBytes(secret); byte[] messageBytes = encoding.GetBytes(message); using (var hmacsha256 = new HMACSHA256(keyByte)) { byte[] hashmessage = hmacsha256.ComputeHash(messageBytes); return Convert.ToBase64String(hashmessage); } }

        public static string HmacSHA256OutHex(string message, string secret) { secret = secret ?? ""; var encoding = new System.Text.UTF8Encoding(); byte[] keyByte = encoding.GetBytes(secret); byte[] messageBytes = encoding.GetBytes(message); using (var hmacsha256 = new HMACSHA256(keyByte)) { byte[] hashmessage = hmacsha256.ComputeHash(messageBytes); return BitConverter.ToString(hashmessage).Replace("-", string.Empty).ToLower(); } }

                private static string Base64(string message) { System.Text.Encoding encode = System.Text.Encoding.UTF8; byte[] bytedata = encode.GetBytes(message); string strPath = Convert.ToBase64String(bytedata, 0, bytedata.Length); return strPath; }
    }
}
