using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MQKJ.BSMP.Utils.Crypt
{
    /// <summary>
    /// 3DES
    /// </summary>
    public static class TripleDESCrypt
    {

        public static string Encrypt(string source, string key)
        {
            TripleDESCryptoServiceProvider desCryptoProvider = new TripleDESCryptoServiceProvider();
            byte[] byteHash;
            byte[] byteBuff;
            byteHash = Encoding.UTF8.GetBytes(key);
            desCryptoProvider.Key = byteHash;
            desCryptoProvider.Mode = CipherMode.CBC;
            desCryptoProvider.IV= Encoding.UTF8.GetBytes(key.Substring(0,8));
            desCryptoProvider.Padding = PaddingMode.PKCS7;
            byteBuff = Encoding.UTF8.GetBytes(source);
            string encoded =
                Convert.ToBase64String(desCryptoProvider.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            return encoded;
        }

        public static string Decrypt(string encodedText, string key)
        {
            TripleDESCryptoServiceProvider desCryptoProvider = new TripleDESCryptoServiceProvider();
            byte[] byteHash;
            byte[] byteBuff;
            byteHash = Encoding.UTF8.GetBytes(key);
            desCryptoProvider.Key = byteHash;
            desCryptoProvider.Mode = CipherMode.CBC;
            desCryptoProvider.IV = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            desCryptoProvider.Padding = PaddingMode.PKCS7;
            byteBuff = Convert.FromBase64String(encodedText);
            string plaintext = Encoding.UTF8.GetString(desCryptoProvider.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            return plaintext;
        }

        public static WechatUserInfoModel AES_Decrypt(string encryptedDataStr, string sessionKey, string iv)
        {
            RijndaelManaged rijalg = new RijndaelManaged();
            //设置 cipher 格式 AES-128-CBC    

            rijalg.KeySize = 128;

            rijalg.Padding = PaddingMode.PKCS7;
            rijalg.Mode = CipherMode.CBC;

            rijalg.Key = Convert.FromBase64String(sessionKey);
            rijalg.IV = Convert.FromBase64String(iv);


            byte[] encryptedData = Convert.FromBase64String(encryptedDataStr);
            //解密    
            ICryptoTransform decryptor = rijalg.CreateDecryptor(rijalg.Key, rijalg.IV);

            string result;

            WechatUserInfoModel model = new WechatUserInfoModel();

            using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        result = srDecrypt.ReadToEnd();
                        model = JsonConvert.DeserializeObject<WechatUserInfoModel>(result);
                    }
                }
            }

            return model;
        }

        public class WechatUserInfoModel
        {
            public string OpenId { get; set; }

            public string NickName { get; set; }
            public int Gender { get; set; }
            public string City { get; set; }
            public string Province { get; set; }

            public string Country { get; set; }

            public string AvatarUrl { get; set; }

            public string UnionId { get; set; }
        }
    }
}
