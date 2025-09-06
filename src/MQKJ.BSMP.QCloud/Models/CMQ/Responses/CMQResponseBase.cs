using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.QCloud.Models.CMQ.Responses
{
    public abstract class CMQResponseBase : QResponseBase
    {
        /// <summary>
        /// 0：表示成功，others：错误，详细错误见下表。
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 错误提示信息。
        /// </summary>
        public string Message { get; set; }
    }
}
