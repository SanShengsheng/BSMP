using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.SceneFiles;


namespace MQKJ.BSMP.SceneFiles.DomainServices
{
    public interface ISceneFileManager : IDomainService
    {
        
        /// <summary>
        /// 初始化方法
        /// </summary>
        void InitSceneFile();


		
		//// custom codes 
		
        //// custom codes end

    }
}
