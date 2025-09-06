using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;


namespace MQKJ.BSMP.Scenes.DomainServices
{
    public interface ISceneManager : IDomainService
    {
        
        /// <summary>
        /// 初始化方法
        /// </summary>
        void InitScene();


		
		//// custom codes 
		
        //// custom codes end

    }
}
