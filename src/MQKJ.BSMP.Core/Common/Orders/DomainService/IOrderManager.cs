

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.Orders;


namespace MQKJ.BSMP.Orders.DomainService
{
    public interface IOrderManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitOrder();



		 
      
         

    }
}
