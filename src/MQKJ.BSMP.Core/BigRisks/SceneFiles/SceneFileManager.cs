using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using MQKJ.BSMP.SceneFiles;

namespace MQKJ.BSMP.SceneFiles.DomainServices
{
    /// <summary>
    /// SceneFile领域层的业务管理
    /// </summary>
    public class SceneFileManager :ISceneFileManager
    {
        private readonly IRepository<SceneFile, Guid> _scenefileRepository;

        /// <summary>
        /// SceneFile的构造方法
        /// </summary>
        public SceneFileManager(IRepository<SceneFile, Guid> scenefileRepository)
        {
            _scenefileRepository = scenefileRepository;
        }
		
		
		/// <summary>
		///     初始化
		/// </summary>
		public void InitSceneFile()
		{
			throw new NotImplementedException();
		}

		//TODO:编写领域业务代码


		
		//// custom codes 
		
        //// custom codes end

    }
}
