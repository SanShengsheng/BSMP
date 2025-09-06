

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
using MQKJ.BSMP.Common.OperationActivities;


namespace MQKJ.BSMP.Common.OperationActivities.DomainService
{
    /// <summary>
    /// OperationActivity领域层的业务管理
    ///</summary>
    public class OperationActivityManager :BSMPDomainServiceBase, IOperationActivityManager
    {
		
		private readonly IRepository<OperationActivity,int> _repository;

		/// <summary>
		/// OperationActivity的构造方法
		///</summary>
		public OperationActivityManager(
			IRepository<OperationActivity, int> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitOperationActivity()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
