using Abp.Application.Services.Dto;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Models;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.TestModels;
using MQKJ.BSMP.ChineseBabies.Athletics;
using MQKJ.BSMP.ChineseBabies.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static MQKJ.BSMP.ChineseBabies.Athetics.Competitions.CompetitionApplicationService;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions
{
    public interface ICompetitionApplicationService : BsmpApplicationService<Competition,Guid,CompetitionEditDto, CompetitionEditDto,GetCompetitionInput, CompetitionListDtos>
    {
        /// <summary>
        /// 竞技场是否开启
        /// </summary>
        /// <returns></returns>
        Task<EnterAtheticsOutput> EnterAthetics(EnterAtheticsInput input);

        /// <summary>
        /// 装备的道具
        /// </summary>
        /// <param name="babyId"></param>
        /// <param name="familyId"></param>
        /// <returns></returns>
        EquipmentPropModel IsEquipmentProp(int babyId, int familyId);

        /// <summary>
        /// 获取段位
        /// </summary>
        /// <param name="babyId"></param>
        /// <param name="familyId"></param>
        /// <returns></returns>
        Task<DanGrading> GetPlayerDanGride(int babyId, int familyId);

        /// <summary>
        /// 开始对战
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StartFightOutput> StartFight(StartFightInput input);

        /// <summary>
        /// 结束对战
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<EndFightOutput> EndFight(EndFightInput input);

        /// <summary>
        /// 获取排行榜列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RankingModel> GetRankingList(GetRankingListInput input);

        /// <summary>
        /// 获取上期的排行
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<RankingModel> GetLatestRankingList(GetRankingListInput input);


        /// <summary>
        /// 获取对战列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetRankingListOutput>> GetFightList(GetRankingListInput input);

        /// <summary>
        /// 购买对战次数
        /// </summary>
        /// <returns></returns>
        Task<BuyFightCountOutput> BuyFightCount(BuyFightCountInput input);

        /// <summary>
        /// 获取消息 
        /// </summary>
        /// <param name="palyerId"></param>
        /// <returns></returns>
        //Task<PagedResultDto<InformationListDto>> GetAthleticsInformations(GetInformationsInput input);
        Task<PagedResultDto<GetAthleticsInformationsListDtos>> GetAthleticsInformations(GetAthleticsInformationsInput input);


        ///// <summary>
        ///// 获取跑马灯消息
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task<string[]> GetRunHorseInformations();

        /// <summary>
        /// 获取当前竞技场信息
        /// </summary>
        /// <returns></returns>
        Task<GetAthleticsInfoOutput> GetAthleticsInfo();

        /// <summary>
        /// 测试获取宝宝随机属性
        /// </summary>
        /// <returns></returns>
        BabyAttributeCode TestRandomAttribute(int babyId);


        /// <summary>
        /// 测试获取对战结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TestFightResultOutput> TestFightResult(TestFightResultInput input);

        Task TimingAwardPrize();
        //Task TimingAwardPrizes();
        //Task TestHanfire();
    }
}
