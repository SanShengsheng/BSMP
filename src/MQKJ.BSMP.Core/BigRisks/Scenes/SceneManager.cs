using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

namespace MQKJ.BSMP.Scenes.DomainServices
{
    /// <summary>
    /// Scene领域层的业务管理
    /// </summary>
    public class SceneManager :ISceneManager
    {
        private readonly IRepository<Scene, int> _sceneRepository;

        /// <summary>
        /// Scene的构造方法
        /// </summary>
        public SceneManager(IRepository<Scene, int> sceneRepository)
        {
            _sceneRepository = sceneRepository;
        }
		
		
		/// <summary>
		///     初始化
		/// </summary>
		public void InitScene()
		{
			throw new NotImplementedException();
		}

		//TODO:编写领域业务代码


		
		//// custom codes 
		
        //// custom codes end

    }
}
