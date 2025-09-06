using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos;
using MQKJ.BSMP.ChineseBabies.Dtos;
using Should;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MQKJ.BSMP.Tests.ChineseBaby.Athletics
{
    public class CompetitionAppService_Tests: BSMPTestBase
    {
        private readonly ICompetitionApplicationService _entityAppService;
        private readonly IBabyAppService _babyAppService;

        public CompetitionAppService_Tests()
        {
            _entityAppService = Resolve<ICompetitionApplicationService>();

            _babyAppService = Resolve<IBabyAppService>();
        }

        /// <summary>
        /// 获取排行榜列表
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetRankingList()
        {
            var baby = await _babyAppService.GetFirstBaby();
            var output = await _entityAppService.GetRankingList(new GetRankingListInput()
            {
                BabyId = baby.Id
            });

            output.ShouldNotBeNull();
        }

        /// <summary>
        /// 获取可选对手列表
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetFightList()
        {
            var baby = await _babyAppService.GetFirstBaby();
            var output = await _entityAppService.GetFightList(new GetRankingListInput()
            {
                BabyId = baby.Id
            });

            output.ShouldNotBeNull();
        }

        /// <summary>
        /// 进入竞技场
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task EnterAthetics()
        {
            var baby = await _babyAppService.GetFirstBaby();
            var output = await _entityAppService.EnterAthetics(new EnterAtheticsInput()
            {
                BabyId = baby.Id,
                FamilyId = baby.FamilyId,
                PlayerId = baby.Family.FatherId
            });

            output.ShouldNotBeNull();
        }

        /// <summary>
        /// 开始对战
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task StartFight()
        {
            var baby = await _babyAppService.GetFirstBaby();
            var output = await _entityAppService.StartFight(new StartFightInput()
            {
                BabyId = baby.Id,
                FamilyId = baby.FamilyId,
                PlayerId = baby.Family.FatherId,
                OtherBabyId = 3
            });

            output.ShouldNotBeNull();
        }

        /// <summary>
        /// 购买对战次数
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BuyFightCount()
        {
            var baby = await _babyAppService.GetFirstBaby();
            var output = await _entityAppService.BuyFightCount(new BuyFightCountInput()
            {
                FamilyId = baby.FamilyId,
                PlayerId = baby.Family.FatherId,
                Count = 10
            });

            output.ShouldNotBeNull();
        }

        /// <summary>
        /// 获取跑马灯消息
        /// </summary>
        /// <returns></returns>
        //[Fact]
        //public async Task GetAthleticsRunHorseInformations()
        //{
        //    var baby = await _babyAppService.GetFirstBaby();
        //    var output = await _entityAppService.GetAthleticsInformations(new GetAthleticsInformationsInput() {

        //    });

        //    output.ShouldNotBeNull();
        //}

        /// <summary>
        /// 获取竞技场消息
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAthleticsInformations()
        {
            var baby = await _babyAppService.GetFirstBaby();
            var output = await _entityAppService.GetAthleticsInformations(new GetAthleticsInformationsInput()
            {
                FamilyId = 1
            });

            output.ShouldNotBeNull();
        }
    }
}
