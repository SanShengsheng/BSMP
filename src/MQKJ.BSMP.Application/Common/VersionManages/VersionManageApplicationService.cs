using Abp.Authorization;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Authorization;
using MQKJ.BSMP.ChineseBabies.DomainService;
using MQKJ.BSMP.Common.Dtos;
using MQKJ.BSMP.Common.VersionManages.Dtos;
using MQKJ.BSMP.Utils.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Common
{
    /// <summary>
    /// VersionManage应用层服务的接口实现方法  
    ///</summary>
    public class VersionManageAppService : BsmpApplicationServiceBase<VersionManage, int, VersionManageEditDto, VersionManageEditDto, GetVersionManagesInput, VersionManageListDto>, IVersionManageAppService
    {
        private readonly IRepository<VersionManage, int> _entityRepository;

        private readonly IVersionManageManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public VersionManageAppService(
        IRepository<VersionManage, int> entityRepository
        , IVersionManageManager entityManager
        ) : base(entityRepository)
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
        }

        //	[AbpAuthorize(VersionManagePermissions.Create,VersionManagePermissions.Edit)]



        /// <summary>
        /// 批量删除VersionManage的方法
        /// </summary>
        [AbpAuthorize(VersionManagePermissions.BatchDelete)]
        public async Task BatchDelete(List<int> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }
       /// <summary>
       /// 获取版本信息
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
        public async Task<VersionManageEditDto> GetLastestVersion ()
        {
            var query = await _entityRepository.GetAll().LastOrDefaultAsync();
            var response = ObjectMapper.Map<VersionManageEditDto>(query);
            return response;
        }

        internal override IQueryable<VersionManage> GetQuery(GetVersionManagesInput input)
        {
            var query = _entityRepository.GetAll();
            return query;
        }
    }
}


