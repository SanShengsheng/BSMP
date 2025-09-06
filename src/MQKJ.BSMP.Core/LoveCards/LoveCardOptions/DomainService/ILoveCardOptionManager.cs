

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.LoveCardOptions;


namespace MQKJ.BSMP.LoveCardOptions.DomainService
{
    public interface ILoveCardOptionManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitLoveCardOption();



		 
      
         

    }
}
