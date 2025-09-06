

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
using MQKJ.BSMP.Likes;


namespace MQKJ.BSMP.Likes.DomainService
{
    /// <summary>
    /// Like领域层的业务管理
    ///</summary>
    public class LikeManager : ILikeManager
    {
		
		private readonly IRepository<Like,Guid> _repository;

		/// <summary>
		/// Like的构造方法
		///</summary>
		public LikeManager(
			IRepository<Like, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLike()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
