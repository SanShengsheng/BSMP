using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common
{
    public class JpushMessageConfig
    {
        public string AppKey { get; set; }

        public string MasterSecret { get; set; }

        public string SendMessageUrl { get; set; }

        public int TempId { get; set; }
    }
}
