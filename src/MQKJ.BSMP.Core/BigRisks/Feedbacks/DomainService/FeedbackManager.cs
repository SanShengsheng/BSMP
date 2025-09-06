

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
using MQKJ.BSMP.Feedbacks;


namespace MQKJ.BSMP.Feedbacks.DomainService
{
    /// <summary>
    /// Feedback领域层的业务管理
    ///</summary>
    public class FeedbackManager : IFeedbackManager
    {
		
		private readonly IRepository<Feedback,Guid> _repository;

		/// <summary>
		/// Feedback的构造方法
		///</summary>
		public FeedbackManager(
			IRepository<Feedback, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitFeedback()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
