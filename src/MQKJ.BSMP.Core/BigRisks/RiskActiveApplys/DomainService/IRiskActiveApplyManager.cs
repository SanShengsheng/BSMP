

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.ActiveApply;


namespace MQKJ.BSMP.ActiveApply.DomainService
{
    public interface IRiskActiveApplyManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitRiskActiveApply();



		 
      
         

    }
}
