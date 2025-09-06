using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.Players;


namespace MQKJ.BSMP.Players.DomainServices
{
    public interface IPlayerManager : IDomainService
    {
        
        /// <summary>
        /// 初始化方法
        /// </summary>
        void InitPlayer();


		
		//// custom codes 
		
        //// custom codes end

    }
}
