using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Utils.JpushTool.Dtos
{
    public class JpushMessageInput
    {
        public string AppKey { get; set; }

        public string MasterSecret { get; set; }

        public string SendMessageUrl { get; set; }

        public string PhoneNumber { get; set; }

        public int TempId { get; set; }

        public string MessageId { get; set; }

        public string Code { get; set; }
    }
}
