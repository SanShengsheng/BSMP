

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.PlayerLabels;


namespace MQKJ.BSMP.PlayerLabels.DomainService
{
    public interface IPlayerLabelManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitPlayerLabel();



		 
      
         

    }
}
