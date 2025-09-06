using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP
{
    /// <summary>
    /// BSMP 最基本的错误类
    /// </summary>
    public class BSMPException : Exception
    {
        public BSMPException():base() { }

        public BSMPException(int code, string message = "", string reason = ""):base (message)
        {
            Code = code;
            Reason = reason;
        }

        public int Code { get; set; }
        public string Reason { get; set; }
    }
}
