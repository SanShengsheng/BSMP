

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
using MQKJ.BSMP.Friends;


namespace MQKJ.BSMP.Friends.DomainService
{
    /// <summary>
    /// Friend领域层的业务管理
    ///</summary>
    public class FriendManager : IFriendManager
    {
		
		private readonly IRepository<Friend,Guid> _repository;

		/// <summary>
		/// Friend的构造方法
		///</summary>
		public FriendManager(
			IRepository<Friend, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitFriend()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
