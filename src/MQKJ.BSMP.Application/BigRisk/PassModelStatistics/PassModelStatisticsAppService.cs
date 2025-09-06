using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.PassModelStatistics.Dtos;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Collections.Extensions;
using MQKJ.BSMP.Friends;
using Abp.Application.Services.Dto;

namespace MQKJ.BSMP.PassModelStatistics
{
    public class PassModelStatisticsAppService : BSMPAppServiceBase, IPassModelStatisticsAppService
    {
        private readonly IRepository<GameTask, Guid> _gameTaskRepository;

        private readonly IRepository<AnswerQuestion, Guid> _answerQuestionRepository;

        private readonly IRepository<Friend, Guid> _friendRepository;

        public PassModelStatisticsAppService(IRepository<GameTask, Guid> gameTaskRepository
                                             ,IRepository<AnswerQuestion,Guid> answerQuestionRepository
                                             , IRepository<Friend, Guid> friendRepository
            )
        {
            _gameTaskRepository = gameTaskRepository;

            _answerQuestionRepository = answerQuestionRepository;

            _friendRepository = friendRepository;
        }

        public List<GetPassModelOpeningDataOutput> GetOpeningData(GetPassModelOpeningDataDto input)
        {
            var query = _gameTaskRepository.GetAll()
                .AsNoTracking()
                .WhereIf(input.StartTime != null && input.EndTime != null, g => g.CreationTime > input.StartTime && g.CreationTime < input.EndTime)
                .Where(x => x.GameType == GameType.EndLess)
                .OrderBy(g => g.CreationTime);

            var gameTasks = query.GroupBy(g => g.CreationTime.Date);

            //var gameIds = query.Select(g => g.Id);

            //var answerQuestions = _answerQuestionRepository.GetAll().
            //    Where(a => gameIds.Contains(a.GameTaskId));

            List<GetPassModelOpeningDataOutput> outputLst = new List<GetPassModelOpeningDataOutput>();

            foreach (var item in gameTasks)
            {
                GetPassModelOpeningDataOutput output = new GetPassModelOpeningDataOutput();

                output.Count = item.Count();

                output.DateTime = item.Key;

                outputLst.Add(output);
            }

            return outputLst;
        }


        public async Task<List<GetPassModelAnswerQuestionDataOutput>> GetAnswerQuestionData(GetPassModelAnswerQuestionDataDto input)
        {
            var gameTaskQuery = _gameTaskRepository.GetAll()
                .AsNoTracking()
                .WhereIf(input.StartTime != null && input.EndTime != null, g => g.CreationTime > input.StartTime && g.CreationTime < input.EndTime)
                .Where(x => x.GameType == GameType.EndLess)
                .OrderBy(g => g.CreationTime);

            var gameIds = gameTaskQuery.Select(x => x.Id).ToList();

            var answerQuestions = await _answerQuestionRepository.GetAll()
                .Where(a => gameIds.Contains(a.GameTaskId))
                .GroupBy(x => x.CreationTime.Date)
                .AsNoTracking()
                .ToListAsync();

            List<GetPassModelAnswerQuestionDataOutput> outputLst = new List<GetPassModelAnswerQuestionDataOutput>();

            foreach (var item in answerQuestions)
            {
                GetPassModelAnswerQuestionDataOutput output = new GetPassModelAnswerQuestionDataOutput();

                output.TotalCount = item.Count();

                output.CheatCount = item.Count(a => a.IsCheat);//作弊答对的数量

                output.RealCount = item.Count(a => a.InviterAnswerSort == a.InviteeAnswerSort);//没作弊答对数量

                output.RightCount = output.CheatCount + output.RealCount;

                output.DateTime = item.Key.Date;

                outputLst.Add(output);
            }

            return outputLst;
        }

        public async Task<PagedResultDto<GetLevelDistributionOutput>> GetLevelDistribution(GetPassModelLevelDistributionDto input)
        {
            if (input.DateTime != null && input.DateTime.ToString() == "0001/1/1 0:00:00")
            {
                input.DateTime = DateTime.Now;
            }
            var startTime = input.DateTime.Date;
            var endTime = input.DateTime.Date.AddDays(1);
            var query = await _friendRepository.GetAll()
                .Where(t => t.LastModificationTime > startTime && t.LastModificationTime < endTime)
                .GroupBy(f => f.Floor)
                .ToListAsync();

            var floors = query.Select(x => x.Key);

            var answerQuestions = /*await*/ _answerQuestionRepository.GetAll()
                .Where(t => t.LastModificationTime > startTime && t.LastModificationTime < endTime && floors.Contains(t.Floor))
                .GroupBy(x => x.Floor);
                //.ToListAsync();

            List<GetLevelDistributionOutput> outputLst = new List<GetLevelDistributionOutput>();

            for (int i = 0; i < query.Count; i++)
            {
                var output = new GetLevelDistributionOutput();

                output.Floor = query[i].Key;

                if (answerQuestions.Count() == 0)
                {
                    output.AveragePassFailCount = 0;

                    output.HighestPassFailCount = 0;
                }
                else
                {
                    output.AveragePassFailCount = Convert.ToInt32(answerQuestions.FirstOrDefault(f => f.Key == query[i].Key).Count()) / query[i].Count();

                    output.HighestPassFailCount = Convert.ToInt32(answerQuestions.Max(x => x.Key == query[i].Key));
                }


                output.FloorCount = query[i].Count();

                outputLst.Add(output);
            }

            return new PagedResultDto<GetLevelDistributionOutput>(
                        outputLst.Count(),
                        outputLst
                );

        }
    }
}
