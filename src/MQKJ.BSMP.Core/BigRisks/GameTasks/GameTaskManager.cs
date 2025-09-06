using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using MQKJ.BSMP.GameTasks;

namespace MQKJ.BSMP.GameTasks.DomainServices
{
    /// <summary>
    /// GameTask领域层的业务管理
    /// </summary>
    public class GameTaskManager : IGameTaskManager
    {
        private readonly IRepository<GameTask, Guid> _gametaskRepository;

        /// <summary>
        /// GameTask的构造方法
        /// </summary>
        public GameTaskManager(IRepository<GameTask, Guid> gametaskRepository)
        {
            _gametaskRepository = gametaskRepository;
        }
		
		
		/// <summary>
		///     初始化
		/// </summary>
		public void InitGameTask()
		{
			throw new NotImplementedException();
		}

		//TODO:编写领域业务代码


		
		//// custom codes 
		
        //// custom codes end

    }
}
