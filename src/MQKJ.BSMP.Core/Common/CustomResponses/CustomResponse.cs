using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.CustomResponses
{
    public class CustomResponse
    {
        public object result { get; set; }

        public string targetUrl { get; set; }

        public bool success { get; set; }

        public string error { get; set; }

        public bool unAuthorizedRequest { get; set; }

        public bool __abp { get; set; }

        //public CustomResponse(dynamic result,string targetUrl,bool success,string error,bool unAuthorizedRequest,bool __abp)
        //{
        //    this.result = result;
        //    this.targetUrl = targetUrl;
        //    this.success = success;
        //    this.error = error;
        //    this.unAuthorizedRequest = unAuthorizedRequest;
        //    this.__abp = __abp;
        //}

        public CustomResponse()
        {
            this.result = new object();
        }
    }
}
