

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.ChineseBabies;


namespace MQKJ.BSMP.ChineseBabies.DomainService
{
    public interface IVersionManageManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitVersionManage();



		 
      
         

    }
}
