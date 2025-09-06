

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.StaminaRecords;


namespace MQKJ.BSMP.StaminaRecords.DomainService
{
    public interface IStaminaRecordManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitStaminaRecord();



		 
      
         

    }
}
