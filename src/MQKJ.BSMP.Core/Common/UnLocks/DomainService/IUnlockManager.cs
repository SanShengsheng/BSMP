

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.UnLocks;


namespace MQKJ.BSMP.UnLocks.DomainService
{
    public interface IUnlockManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitUnlock();



		 
      
         

    }
}
