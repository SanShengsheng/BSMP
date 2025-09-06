using System;

namespace MQKJ.BSMP.QCloud.Models.MQAPI
{
    /// <summary>
    /// Api返回模型
    /// </summary>
    public class MQApiResponseModel : ResponseBase
    {
        /// <summary>
        /// 是否有错误
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// 错误编码
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// 错误信息 显示给用户看
        /// </summary>
        public String ErrorMessage { get; set; }
        /// <summary>
        /// 错误理由，一般是Trace信息
        /// </summary>
        public String ErrorReason { get; set; }

        public override string ToString()
        {
            if (IsError)
            {
                return $"has error, code is {ErrorCode}, message is {ErrorMessage}, reason is {ErrorReason}";
            }
            return base.ToString();
        }
    }

    /// <summary>
    /// 带实体结果的Api返回类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class MQApiResponseModel<TEntity> : MQApiResponseModel
    {
        public TEntity Data { get; set; }
    }
    
}