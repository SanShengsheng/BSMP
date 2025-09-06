using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;

using System.Linq.Dynamic.Core;
 using Microsoft.EntityFrameworkCore; 


using MQKJ.BSMP.AnswerQuestions.Dtos;
using MQKJ.BSMP.GameTasks;
using System;

namespace MQKJ.BSMP.AnswerQuestions
{
    /// <summary>
    /// AnswerQuestion应用层服务的接口实现方法
    /// </summary>
	
    public class AnswerQuestionAppService : BSMPAppServiceBase, IAnswerQuestionAppService
    {
		private readonly IRepository<AnswerQuestion, Guid> _answerquestionRepository;

		
		
		/// <summary>
		/// 构造函数
		/// </summary>
		public AnswerQuestionAppService(
			IRepository<AnswerQuestion, Guid> answerquestionRepository
			
		)
		{
			_answerquestionRepository = answerquestionRepository;
			
		}
		
		
		/// <summary>
		/// 获取AnswerQuestion的分页列表信息
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public  async  Task<PagedResultDto<AnswerQuestionListDto>> GetPagedAnswerQuestions(GetAnswerQuestionsInput input)
		{
		    
		    var query = _answerquestionRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
		
			var answerquestionCount = await query.CountAsync();
		
			var answerquestions = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();
		
				// var answerquestionListDtos = ObjectMapper.Map<List <AnswerQuestionListDto>>(answerquestions);
				var answerquestionListDtos =answerquestions.MapTo<List<AnswerQuestionListDto>>();
		
				return new PagedResultDto<AnswerQuestionListDto>(
							answerquestionCount,
							answerquestionListDtos
					);
		}
		

		/// <summary>
		/// 通过指定id获取AnswerQuestionListDto信息
		/// </summary>
		public async Task<AnswerQuestionListDto> GetAnswerQuestionByIdAsync(EntityDto<Guid> input)
		{
			var entity = await _answerquestionRepository.GetAsync(input.Id);
		
		    return entity.MapTo<AnswerQuestionListDto>();
		}
		
		/// <summary>
		/// MPA版本才会用到的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async  Task<GetAnswerQuestionForEditOutput> GetAnswerQuestionForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetAnswerQuestionForEditOutput();
			AnswerQuestionEditDto answerquestionEditDto;
		
			if (input.Id.HasValue)
			{
				var entity = await _answerquestionRepository.GetAsync(input.Id.Value);
		
				answerquestionEditDto = entity.MapTo<AnswerQuestionEditDto>();
		
				//answerquestionEditDto = ObjectMapper.Map<List <answerquestionEditDto>>(entity);
			}
			else
			{
				answerquestionEditDto = new AnswerQuestionEditDto();
			}
		
			output.AnswerQuestion = answerquestionEditDto;
			return output;
		}
		
		
		/// <summary>
		/// 添加或者修改AnswerQuestion的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task CreateOrUpdateAnswerQuestion(CreateOrUpdateAnswerQuestionInput input)
		{
		    
			if (input.AnswerQuestion.Id.HasValue)
			{
				await UpdateAnswerQuestionAsync(input.AnswerQuestion);
			}
			else
			{
				await CreateAnswerQuestionAsync(input.AnswerQuestion);
			}
		}
		

		/// <summary>
		/// 新增AnswerQuestion
		/// </summary>
		
		protected virtual async Task<AnswerQuestionEditDto> CreateAnswerQuestionAsync(AnswerQuestionEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增
		
			var entity = ObjectMapper.Map <AnswerQuestion>(input);
		
			entity = await _answerquestionRepository.InsertAsync(entity);
			return entity.MapTo<AnswerQuestionEditDto>();
		}
		
		/// <summary>
		/// 编辑AnswerQuestion
		/// </summary>
		
		protected virtual async Task UpdateAnswerQuestionAsync(AnswerQuestionEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新
		
			var entity = await _answerquestionRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);
		
			// ObjectMapper.Map(input, entity);
		    await _answerquestionRepository.UpdateAsync(entity);
		}
		

		
		/// <summary>
		/// 删除AnswerQuestion信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task DeleteAnswerQuestion(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _answerquestionRepository.DeleteAsync(input.Id);
		}
		
		
		
		/// <summary>
		/// 批量删除AnswerQuestion的方法
		/// </summary>
		
		public async Task BatchDeleteAnswerQuestionsAsync(List<Guid> input)
		{
			//TODO:批量删除前的逻辑判断，是否允许删除
			await _answerquestionRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        public async Task<IEnumerable<AnswerQuestion>> FindUserAnswerQuestions(FindUserAnswerQuestionsRequest request)
        {
            return await _answerquestionRepository.GetAllIncluding(a => a.GameTask)
                .Where(a => a.GameTask.InviterPlayerId == request.InviterPlayId &&
                            a.GameTask.InviteePlayerId == request.InviteePlayId)
                .ToListAsync();
        }


        /// <summary>
		/// 导出AnswerQuestion为excel表
		/// </summary>
		/// <returns></returns>
		//public async Task<FileDto> GetAnswerQuestionsToExcel()
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


 