
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MQKJ.BSMP.Utils.Tools
{
   public static class FilterHtmlHelp
    {
        public static string NoHtml(string htmlString)
        {
            //删除脚本
            htmlString = Regex.Replace(htmlString, @"<script[^>]*?>.*?</script>", "",
              RegexOptions.IgnoreCase);
            //删除HTML
            htmlString = Regex.Replace(htmlString, @"<(.[^>]*)>", "",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"([\r\n])[\s]+", "",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"-->", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"<!--.*", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(quot|#34);", "\"",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(amp|#38);", "&",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(lt|#60);", "<",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(gt|#62);", ">",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(nbsp|#160);", "   ",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(iexcl|#161);", "\xa1",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(cent|#162);", "\xa2",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(pound|#163);", "\xa3",
              RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&#(\d+);", "",
              RegexOptions.IgnoreCase);

            htmlString.Replace("<", "");
            htmlString.Replace(">", "");
            htmlString.Replace("\r\n", "");
            //htmlString = HttpContext.Current.Server.HtmlEncode(htmlString).Trim();

            return htmlString;
        }
    }
}
