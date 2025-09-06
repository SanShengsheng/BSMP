

using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Linq;
using Abp.Linq.Extensions;
using Abp.Extensions;
using Abp.UI;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

using MQKJ.BSMP;
using MQKJ.BSMP.UnLocks;


namespace MQKJ.BSMP.UnLocks.DomainService
{
    /// <summary>
    /// Unlock领域层的业务管理
    ///</summary>
    public class UnlockManager :BSMPDomainServiceBase, IUnlockManager
    {
		
		private readonly IRepository<Unlock,Guid> _repository;

		/// <summary>
		/// Unlock的构造方法
		///</summary>
		public UnlockManager(
			IRepository<Unlock, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitUnlock()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
