

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
using MQKJ.BSMP.LoveCardOptions;


namespace MQKJ.BSMP.LoveCardOptions.DomainService
{
    /// <summary>
    /// LoveCardOption领域层的业务管理
    ///</summary>
    public class LoveCardOptionManager :BSMPDomainServiceBase, ILoveCardOptionManager
    {
		
		private readonly IRepository<LoveCardOption,Guid> _repository;

		/// <summary>
		/// LoveCardOption的构造方法
		///</summary>
		public LoveCardOptionManager(
			IRepository<LoveCardOption, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLoveCardOption()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
