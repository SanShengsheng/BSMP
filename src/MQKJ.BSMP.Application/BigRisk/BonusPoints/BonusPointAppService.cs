using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;

using System.Linq.Dynamic.Core;
 using Microsoft.EntityFrameworkCore; 
using MQKJ.BSMP.BonusPoints.Authorization;
using MQKJ.BSMP.BonusPoints.DomainServices;
using MQKJ.BSMP.BonusPoints.Dtos;
using MQKJ.BSMP.BonusPoints;

namespace MQKJ.BSMP.BonusPoints
{
    /// <summary>
    /// BonusPoint应用层服务的接口实现方法
    /// </summary>

    public class BonusPointAppService : BSMPAppServiceBase, IBonusPointAppService
    {
		private readonly IRepository<BonusPoint, int> _bonuspointRepository;

		private readonly IBonusPointManager _bonuspointManager;
		
		/// <summary>
		/// 构造函数
		/// </summary>
		public BonusPointAppService(
			IRepository<BonusPoint, int> bonuspointRepository
			,IBonusPointManager bonuspointManager
		)
		{
			_bonuspointRepository = bonuspointRepository;
			 _bonuspointManager=bonuspointManager;
		}
		
		
		/// <summary>
		/// 获取BonusPoint的分页列表信息
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public  async  Task<PagedResultDto<BonusPointListDto>> GetPagedBonusPoints(GetBonusPointsInput input)
		{
		    
		    var query = _bonuspointRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
		
			var bonuspointCount = await query.CountAsync();
		
			var bonuspoints = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();
		
				// var bonuspointListDtos = ObjectMapper.Map<List <BonusPointListDto>>(bonuspoints);
				var bonuspointListDtos =bonuspoints.MapTo<List<BonusPointListDto>>();
		
				return new PagedResultDto<BonusPointListDto>(
							bonuspointCount,
							bonuspointListDtos
					);
		}
		

		/// <summary>
		/// 通过指定id获取BonusPointListDto信息
		/// </summary>
		public async Task<BonusPointListDto> GetBonusPointByIdAsync(EntityDto<int> input)
		{
			var entity = await _bonuspointRepository.GetAsync(input.Id);
		
		    return entity.MapTo<BonusPointListDto>();
		}
		
		/// <summary>
		/// MPA版本才会用到的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async  Task<GetBonusPointForEditOutput> GetBonusPointForEdit(NullableIdDto<int> input)
		{
			var output = new GetBonusPointForEditOutput();
			BonusPointEditDto bonuspointEditDto;
		
			if (input.Id.HasValue)
			{
				var entity = await _bonuspointRepository.GetAsync(input.Id.Value);
		
				bonuspointEditDto = entity.MapTo<BonusPointEditDto>();
		
				//bonuspointEditDto = ObjectMapper.Map<List <bonuspointEditDto>>(entity);
			}
			else
			{
				bonuspointEditDto = new BonusPointEditDto();
			}
		
			output.BonusPoint = bonuspointEditDto;
			return output;
		}
		
		
		/// <summary>
		/// 添加或者修改BonusPoint的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task CreateOrUpdateBonusPoint(CreateOrUpdateBonusPointInput input)
		{
		    
			if (input.BonusPoint.Id.HasValue)
			{
				await UpdateBonusPointAsync(input.BonusPoint);
			}
			else
			{
				await CreateBonusPointAsync(input.BonusPoint);
			}
		}
		

		/// <summary>
		/// 新增BonusPoint
		/// </summary>
		[AbpAuthorize(BonusPointAppPermissions.BonusPoint_CreateBonusPoint)]
		protected virtual async Task<BonusPointEditDto> CreateBonusPointAsync(BonusPointEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增
		
			var entity = ObjectMapper.Map <BonusPoint>(input);
		
			entity = await _bonuspointRepository.InsertAsync(entity);
			return entity.MapTo<BonusPointEditDto>();
		}
		
		/// <summary>
		/// 编辑BonusPoint
		/// </summary>
		[AbpAuthorize(BonusPointAppPermissions.BonusPoint_EditBonusPoint)]
		protected virtual async Task UpdateBonusPointAsync(BonusPointEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新
		
			var entity = await _bonuspointRepository.GetAsync(input.Id.Value);

            input.MapTo(entity);

            await _bonuspointRepository.UpdateAsync(entity);
		}
		

		
		/// <summary>
		/// 删除BonusPoint信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(BonusPointAppPermissions.BonusPoint_DeleteBonusPoint)]
		public async Task DeleteBonusPoint(EntityDto<int> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _bonuspointRepository.DeleteAsync(input.Id);
		}
		
		
		
		/// <summary>
		/// 批量删除BonusPoint的方法
		/// </summary>
		[AbpAuthorize(BonusPointAppPermissions.BonusPoint_BatchDeleteBonusPoints)]
		public async Task BatchDeleteBonusPointsAsync(List<int> input)
		{
			//TODO:批量删除前的逻辑判断，是否允许删除
			await _bonuspointRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        private static Dictionary<string, BonusPoint> _eventNameBonusPoints;

        public async Task<BonusPoint> GetBounsPointByEventName(string eventName)
        {
            await  EnsureLoadEventNameBonusPoint();
            return _eventNameBonusPoints.ContainsKey(eventName) ? _eventNameBonusPoints[eventName] : null;
        }

        private async Task EnsureLoadEventNameBonusPoint()
        {
            if (_eventNameBonusPoints == null)
            {
                _eventNameBonusPoints = await _bonuspointRepository.GetAll().ToDictionaryAsync(k => k.EventName);
            }
        }

        public async Task<IEnumerable<BonusPointListDto>> GetAllScenesAsync(GetBonusPointsInput input)
        {
            var bonuspoints = await _bonuspointRepository.GetAll().OrderBy(input.Sorting).ToListAsync();
            var dtos = bonuspoints.MapTo<List<BonusPointListDto>>();
            return dtos;
        }

        /// <summary>
        /// 导出BonusPoint为excel表
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetBonusPointsToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}



        //// custom codes 

        //// custom codes end

    }
}


 