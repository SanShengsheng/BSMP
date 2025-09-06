using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.Friends;
using MQKJ.BSMP.GamblingModelStatistics.Dtos;
using MQKJ.BSMP.GameTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.GamblingModelStatistics
{
    public class GamblingModelAppService: BSMPAppServiceBase, IGamblingModelAppService
    {
        private readonly IRepository<GameTask, Guid> _gameTaskRepository;

        private readonly IRepository<AnswerQuestion, Guid> _answerQuestionRepository;

        public GamblingModelAppService(IRepository<GameTask, Guid> gameTaskRepository
                                             , IRepository<AnswerQuestion, Guid> answerQuestionRepository
            )
        {
            _gameTaskRepository = gameTaskRepository;

            _answerQuestionRepository = answerQuestionRepository;
        }

        public List<GetGamblingModelOpeningDataOutput> GetOpeningData(GetGamblingModelOpeningDataDto input)
        {
            var query = _gameTaskRepository.GetAll()
                .AsNoTracking()
                .WhereIf(input.StartTime != null && input.EndTime != null, g => g.CreationTime > input.StartTime && g.CreationTime < input.EndTime)
                .Where(x => x.GameType == GameType.UnKnow) //这里这样写
                .OrderBy(g => g.CreationTime);

            var gameTasks = query.GroupBy(g => g.CreationTime.Date);

            List<GetGamblingModelOpeningDataOutput> outputLst = new List<GetGamblingModelOpeningDataOutput>();

            foreach (var item in gameTasks)
            {
                GetGamblingModelOpeningDataOutput output = new GetGamblingModelOpeningDataOutput();

                output.Count = item.Count();

                output.DateTime = item.Key;

                outputLst.Add(output);
            }

            return outputLst;
        }


        public async Task<List<GetGamblingModelAnswerQuestionDataOutput>> GetAnswerQuestionData(GetGamblingModelAnswerQuestionDataDto input)
        {
            var gameTaskQuery = _gameTaskRepository.GetAll()
                .AsNoTracking()
                .WhereIf(input.StartTime != null && input.EndTime != null, g => g.CreationTime > input.StartTime && g.CreationTime < input.EndTime)
                .Where(x => x.GameType == GameType.UnKnow)
                .OrderBy(g => g.CreationTime);

            var gameIds = gameTaskQuery.Select(x => x.Id).ToList();

            var answerQuestions = await _answerQuestionRepository.GetAll()
                .Include(g => g.GameTask)
                .Where(g => gameIds.Contains(g.GameTaskId))
                //.WhereIf(input.StartTime != null && input.EndTime != null, g => g.CreationTime > input.StartTime && g.CreationTime < input.EndTime)
                .GroupBy(x => x.CreationTime.Date)
                .ToListAsync();

            List<GetGamblingModelAnswerQuestionDataOutput> outputLst = new List<GetGamblingModelAnswerQuestionDataOutput>();

            foreach (var item in answerQuestions)
            {
                GetGamblingModelAnswerQuestionDataOutput output = new GetGamblingModelAnswerQuestionDataOutput();

                output.Count = item.Count();

                output.ContinueCount = item.Count(g => g.GameTask.State == TaskState.TaskAgain);

                output.ThreeQuestionCount = item.Count(g => g.GameTask.TaskType == TaskType.ThreeQuesion);

                output.FiveQuestionCount = item.Count(g => g.GameTask.TaskType == TaskType.FiveQuestion);

                output.TenQuestionCount = item.Count(g => g.GameTask.TaskType == TaskType.TenQuestion);

                output.NormaleCount = item.Count(g => g.GameTask.RelationDegree == RelationDegree.Ordinary);

                output.ShadyCount = item.Count(g => g.GameTask.RelationDegree == RelationDegree.Ambiguous);

                output.LoversCount = item.Count(g => g.GameTask.RelationDegree == RelationDegree.Lovers);

                output.WifeCount = item.Count(g => g.GameTask.RelationDegree == RelationDegree.Couple);

                output.DateTime = item.Key.Date;

                outputLst.Add(output);
            }

            return outputLst;
        }

        public PagedResultDto<GetErrQuestionDistributionOutput> GetErrQuestionDistribution(GetErrQuestionDistributionDto input)
        {
            var query = _answerQuestionRepository.GetAll().Include(g => g.GameTask)
                .AsNoTracking()
                .WhereIf(input.DateTime != null, g => g.GameTask.CreationTime > input.DateTime.Date && g.GameTask.CreationTime < input.DateTime.Date.AddDays(1))
                .Where(x => x.GameTask.GameType == GameType.UnKnow);


            var result = query.GroupBy(a => new { a.GameTaskId, a.GameTask.TaskType })
                .Select(g => new GetErrQuestionDistributionOutput()
                {
                    TaskType = (int)g.Key.TaskType,
                    RelationCount = g.Count(),
                    AverageAnswerErrQuestionCount  = g.Count(a => a.InviteeAnswerSort != a.InviterAnswerSort),
                    HighestAnswerErrQuestionCount = g.Count(a => a.InviteeAnswerSort != a.InviterAnswerSort)
                });

            //foreach (var item in answerQuestions)
            //{
            //    var output = new GetErrQuestionDistributionOutput();

            //    output.TaskType = (int)item.Key;

            //    output.HighestAnswerErrQuestionCount
            //}

            return new PagedResultDto<GetErrQuestionDistributionOutput>(
                       result.ToList().Count(),
                       result.ToList()
               );

        }
    }
}
