

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
using MQKJ.BSMP.Common.SensitiveWords;


namespace MQKJ.BSMP.Common.SensitiveWords.DomainService
{
    /// <summary>
    /// SensitiveWord领域层的业务管理
    ///</summary>
    public class SensitiveWordManager :BSMPDomainServiceBase, ISensitiveWordManager
    {
		
		private readonly IRepository<SensitiveWord,int> _repository;

		/// <summary>
		/// SensitiveWord的构造方法
		///</summary>
		public SensitiveWordManager(
			IRepository<SensitiveWord, int> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitSensitiveWord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
