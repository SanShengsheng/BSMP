

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.Common.SensitiveWords;


namespace MQKJ.BSMP.Common.SensitiveWords.DomainService
{
    public interface ISensitiveWordManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitSensitiveWord();



		 
      
         

    }
}
