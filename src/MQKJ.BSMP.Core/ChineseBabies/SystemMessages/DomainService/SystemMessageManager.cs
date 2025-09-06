

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
using MQKJ.BSMP.SystemMessages;


namespace MQKJ.BSMP.SystemMessages.DomainService
{
    /// <summary>
    /// SystemMessage领域层的业务管理
    ///</summary>
    public class SystemMessageManager :BSMPDomainServiceBase, ISystemMessageManager
    {
		
		private readonly IRepository<SystemMessage,int> _repository;

		/// <summary>
		/// SystemMessage的构造方法
		///</summary>
		public SystemMessageManager(
			IRepository<SystemMessage, int> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitSystemMessage()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
