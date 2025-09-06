using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.Players.DomainServices
{
    /// <summary>
    /// Player领域层的业务管理
    /// </summary>
    public class PlayerManager :IPlayerManager
    {
        private readonly IRepository<Player, Guid> _playerRepository;

        /// <summary>
        /// Player的构造方法
        /// </summary>
        public PlayerManager(IRepository<Player, Guid> playerRepository)
        {
            _playerRepository = playerRepository;
        }
		
		
		/// <summary>
		///     初始化
		/// </summary>
		public void InitPlayer()
		{
			throw new NotImplementedException();
		}

		//TODO:编写领域业务代码


		
		//// custom codes 
		
        //// custom codes end

    }
}
