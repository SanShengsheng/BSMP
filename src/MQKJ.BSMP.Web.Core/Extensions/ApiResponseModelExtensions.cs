using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Logging;
using MQKJ.BSMP.Models;

namespace MQKJ.BSMP
{
    public static class ApiResponseModelExtensions
    {
        public static ApiResponseModel ApiAction(this object obj, Action action)
        {
            var response = new ApiResponseModel();
            try
            {
                action();
            }
            catch (Exception ex)
            {
                CatchException(response, ex);
            }

            return response;
        }

        public static ApiResponseModel<T> ApiFunc<T>(this object obj, Func<T> func)
        {
            var response = new ApiResponseModel<T>();
            try
            {
                response.Data = func();
            }
            catch (Exception ex)
            {
                CatchException(response, ex);
            }

            return response;
        }

        public static async Task<ApiResponseModel> ApiTaskAction(this object obj, Task actionTask)
        {
            var response = new ApiResponseModel();
            try
            {
                await actionTask;
            }
            catch (Exception ex)
            {
                CatchException(response, ex);
            }

            return response;
        }

        public static async Task<ApiResponseModel<T>> ApiTaskFunc<T>(this object obj, Task<T> funcTask)
        {
            var response = new ApiResponseModel<T>();
            try
            {
                response.Data = await funcTask;
            }
            catch (Exception ex)
            {
                CatchException(response, ex);
            }

            return response;
        }

        private static void  CatchException(ApiResponseModel response, Exception ex)
        {
            if (response == null)
            {
                response = new ApiResponseModel();
            }

            response.IsError = true;
            LogHelper.Logger.Error(ex.Message, ex);
            if (ex is BSMPException bsmpException)
            {
                response.ErrorCode = bsmpException.Code;
                response.ErrorMessage = bsmpException.Message;
                response.ErrorReason = bsmpException.Reason;
            }
            else
            {
                response.ErrorCode = -9999;
                response.ErrorMessage = ex.Message;
                response.ErrorReason = ex.ToString();
            }
            
        }

    }
}
