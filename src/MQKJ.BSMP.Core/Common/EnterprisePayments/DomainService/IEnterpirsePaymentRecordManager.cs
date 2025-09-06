

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.Common;


namespace MQKJ.BSMP.Common.DomainService
{
    public interface IEnterpirsePaymentRecordManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitEnterpirsePaymentRecord();



		 
      
         

    }
}
