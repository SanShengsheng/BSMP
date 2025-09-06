

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.ApplicationLogs;


namespace MQKJ.BSMP.ApplicationLogs.DomainService
{
    public interface IApplicationLogManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitApplicationLog();



		 
      
         

    }
}
