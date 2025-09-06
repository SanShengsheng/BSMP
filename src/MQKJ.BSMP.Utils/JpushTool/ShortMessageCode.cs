using Microsoft.AspNetCore.Http;
using MQKJ.BSMP.Utils.JpushTool.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Utils.Tools
{
    public class ShortMessageCode
    {
        public static async Task<string> SendMessageCode(JpushMessageInput input)
        {
            var result = string.Empty;

            var response = string.Empty;

            try
            {

                JObject json = new JObject
            {
                { "mobile", input.PhoneNumber },
                { "temp_id", input.TempId }
            };

                response = await JpushPost(input, json);

                var msgIdObj = JToken.Parse(response);

                result = msgIdObj["msg_id"] == null ? null : msgIdObj["msg_id"].ToString();

            }
            catch (Exception exp)
            {

                throw new Exception($"获取验证码失败,异常信息：{exp}reason:{response}");
            }
            return result;
        }

        /// <summary>
        /// 验证验证码是否有效
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<bool> CodeIsValide(JpushMessageInput input)
        {
            if (string.IsNullOrEmpty(input.Code))
            {
                throw new ArgumentException(nameof(input.Code));
            }
            if (string.IsNullOrEmpty(input.MessageId))
            {
                throw new ArgumentException(nameof(input.MessageId));
            }

            JObject json = new JObject
            {
                { "code", input.Code }
            };

            input.SendMessageUrl += $"/{input.MessageId}/valid";

            string response = await JpushPost(input,json);

            var result = Convert.ToBoolean(JToken.Parse(response)["is_valid"]);

            return result;
        }

        private static async Task<string> JpushPost(JpushMessageInput input,JObject json)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(input.AppKey + ":" + input.MasterSecret));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
            HttpContent httpContent = new StringContent(json.ToString());
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(input.SendMessageUrl, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            return httpResponseContent;
        }
    }
}
