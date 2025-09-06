
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


using MQKJ.BSMP.Likes;
using MQKJ.BSMP.Likes.Dtos;
using MQKJ.BSMP.Likes.DomainService;
using MQKJ.BSMP.Likes.Authorization;


namespace MQKJ.BSMP.Likes
{
    /// <summary>
    /// Like应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class LikeAppService : BSMPAppServiceBase, ILikeAppService
    {
        private readonly IRepository<Like, Guid> _entityRepository;

        private readonly ILikeManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LikeAppService(
        IRepository<Like, Guid> entityRepository
        ,ILikeManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取Like的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		//[AbpAuthorize(LikePermissions.Query)] 
        public async Task<PagedResultDto<LikeListDto>> GetPaged(GetLikesInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LikeListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LikeListDto>>();

			return new PagedResultDto<LikeListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LikeListDto信息
		/// </summary>
		//[AbpAuthorize(LikePermissions.Query)] 
		public async Task<LikeListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LikeListDto>();
		}

		/// <summary>
		/// 获取编辑 Like
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		//[AbpAuthorize(LikePermissions.Create,LikePermissions.Edit)]
		public async Task<GetLikeForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLikeForEditOutput();
LikeEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LikeEditDto>();

				//likeEditDto = ObjectMapper.Map<List<likeEditDto>>(entity);
			}
			else
			{
				editDto = new LikeEditDto();
			}

			output.Like = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改Like的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		//[AbpAuthorize(LikePermissions.Create,LikePermissions.Edit)]
		public async Task CreateOrUpdate(CreateOrUpdateLikeInput input)
		{

            //if (input.Like.Id.HasValue)
            //{
            //	await Update(input.Like);
            //}
            //else
            //{
            //	await Create(input.Like);
            //}

            var entity = await _entityRepository.FirstOrDefaultAsync(l => l.PlayerId == input.Like.PlayerId && l.QuestionId == input.Like.QuestionId);

            if (entity == null)
            {
                await Create(input.Like);
            }
            else
            {
                input.Like.Id = entity.Id;
                input.Like.MapTo(entity);

                 await _entityRepository.UpdateAsync(entity);
            }
        }


		/// <summary>
		/// 新增Like
		/// </summary>
		//[AbpAuthorize(LikePermissions.Create)]
		protected virtual async Task<LikeEditDto> Create(LikeEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Like>(input);
            var entity=input.MapTo<Like>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LikeEditDto>();
		}

		/// <summary>
		/// 编辑Like
		/// </summary>
		//[AbpAuthorize(LikePermissions.Edit)]
		protected virtual async Task Update(LikeEditDto input)
		{
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}

        public async Task<LikeListDto> GetLikeState(GetLikeStateInput input)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(l => l.PlayerId == input.PlayerId && l.QuestionId == input.QuestionId);

            return entity.MapTo<LikeListDto>();
        }

        /// <summary>
        /// 删除Like信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[AbpAuthorize(LikePermissions.Delete)]
        public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除Like的方法
		/// </summary>
		//[AbpAuthorize(LikePermissions.BatchDelete)]
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出Like为excel表,等待开发。
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


