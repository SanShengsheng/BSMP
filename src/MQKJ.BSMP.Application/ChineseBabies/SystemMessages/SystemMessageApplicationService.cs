
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using MQKJ.BSMP.SystemMessages;
using MQKJ.BSMP.SystemMessages.Dtos;
using MQKJ.BSMP.SystemMessages.DomainService;



namespace MQKJ.BSMP.SystemMessages
{
    /// <summary>
    /// SystemMessage应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class SystemMessageAppService : BsmpApplicationServiceBase<SystemMessage, int, SystemMessageEditDto, SystemMessageEditDto, GetSystemMessagesInput, SystemMessageListDto>, ISystemMessageAppService
    {
        private readonly IRepository<SystemMessage, int> _entityRepository;

        private readonly ISystemMessageManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public SystemMessageAppService(
        IRepository<SystemMessage, int> entityRepository
        ,ISystemMessageManager entityManager
        ) : base(entityRepository)
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


      
		/// <summary>
		/// 批量删除SystemMessage的方法
		/// </summary>
		
		public async Task BatchDelete(List<int> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        internal override IQueryable<SystemMessage> GetQuery(GetSystemMessagesInput model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 导出SystemMessage为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}

    }
}


