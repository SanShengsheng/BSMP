

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.SystemMessages;


namespace MQKJ.BSMP.SystemMessages.DomainService
{
    public interface ISystemMessageManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitSystemMessage();



		 
      
         

    }
}
