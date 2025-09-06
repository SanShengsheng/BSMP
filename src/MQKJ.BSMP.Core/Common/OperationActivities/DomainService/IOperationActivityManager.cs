

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.Common.OperationActivities;


namespace MQKJ.BSMP.Common.OperationActivities.DomainService
{
    public interface IOperationActivityManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitOperationActivity();



		 
      
         

    }
}
