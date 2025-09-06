using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Utils.Message.Dtos
{
   public class WechatTemplateMessageDataDto
    {
        public string touser { get; set; }

        public string template_id { get; set; }

        public string page { get; set; }

        public string form_id { get; set; }

        public object data { get; set; }

        public string emphasis_keyword { get; set; }
    }
}
