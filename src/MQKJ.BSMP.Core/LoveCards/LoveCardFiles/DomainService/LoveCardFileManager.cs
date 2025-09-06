

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
using MQKJ.BSMP.LoveCardFiles;


namespace MQKJ.BSMP.LoveCardFiles.DomainService
{
    /// <summary>
    /// LoveCardFile领域层的业务管理
    ///</summary>
    public class LoveCardFileManager :BSMPDomainServiceBase, ILoveCardFileManager
    {
		
		private readonly IRepository<LoveCardFile,Guid> _repository;

		/// <summary>
		/// LoveCardFile的构造方法
		///</summary>
		public LoveCardFileManager(
			IRepository<LoveCardFile, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLoveCardFile()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
