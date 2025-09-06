

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
using MQKJ.BSMP.Common;


namespace MQKJ.BSMP.Common.DomainService
{
    /// <summary>
    /// EnterpirsePaymentRecord领域层的业务管理
    ///</summary>
    public class EnterpirsePaymentRecordManager :BSMPDomainServiceBase, IEnterpirsePaymentRecordManager
    {
		
		private readonly IRepository<EnterpirsePaymentRecord,Guid> _repository;

		/// <summary>
		/// EnterpirsePaymentRecord的构造方法
		///</summary>
		public EnterpirsePaymentRecordManager(
			IRepository<EnterpirsePaymentRecord, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitEnterpirsePaymentRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
