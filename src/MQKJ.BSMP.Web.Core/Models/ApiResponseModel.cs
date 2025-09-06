using System;
using Abp.Application.Services.Dto;

namespace MQKJ.BSMP.Models
{
    /// <summary>
    /// Api返回模型
    /// </summary>
    public class ApiResponseModel
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
    public class ApiResponseModel<TEntity> : ApiResponseModel
    {
        public TEntity Data { get; set; }
    }

    /// <summary>
    /// 实体结果是翻页数据的
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ApiResponsePagedModel<TEntity> : ApiResponseModel<PagedResultDto<TEntity>>
    {

    }
}