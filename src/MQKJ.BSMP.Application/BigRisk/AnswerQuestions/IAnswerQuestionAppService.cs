using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.AnswerQuestions.Dtos;
using MQKJ.BSMP.GameTasks;

namespace MQKJ.BSMP.AnswerQuestions
{
    /// <summary>
    /// AnswerQuestion应用层服务的接口方法
    /// </summary>
    public interface IAnswerQuestionAppService : IApplicationService
    {
        /// <summary>
        /// 获取AnswerQuestion的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<AnswerQuestionListDto>> GetPagedAnswerQuestions(GetAnswerQuestionsInput input);

		/// <summary>
		/// 通过指定id获取AnswerQuestionListDto信息
		/// </summary>
		Task<AnswerQuestionListDto> GetAnswerQuestionByIdAsync(EntityDto<Guid> input);


        /// <summary>
        /// 导出AnswerQuestion为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetAnswerQuestionsToExcel();

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetAnswerQuestionForEditOutput> GetAnswerQuestionForEdit(NullableIdDto<Guid> input);

        //todo:缺少Dto的生成GetAnswerQuestionForEditOutput


        /// <summary>
        /// 添加或者修改AnswerQuestion的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateAnswerQuestion(CreateOrUpdateAnswerQuestionInput input);


        /// <summary>
        /// 删除AnswerQuestion信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteAnswerQuestion(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除AnswerQuestion
        /// </summary>
        Task BatchDeleteAnswerQuestionsAsync(List<Guid> input);


		//// custom codes 


        Task<IEnumerable<AnswerQuestion>> FindUserAnswerQuestions(FindUserAnswerQuestionsRequest request);

        
        //// custom codes end
    }
}
