using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.GameTasks;


namespace MQKJ.BSMP.GameTasks.DomainServices
{
    public interface IGameTaskManager : IDomainService
    {
        
        /// <summary>
        /// 初始化方法
        /// </summary>
        void InitGameTask();


		
		//// custom codes 
		
        //// custom codes end

    }
}
