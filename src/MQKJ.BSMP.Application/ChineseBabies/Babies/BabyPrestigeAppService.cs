using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos.PrestigeDtos;
using MQKJ.BSMP.ChineseBabies.Prestiges;

using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp;
using Abp.Linq.Extensions;
using System.Linq.Expressions;
using Abp.Domain.Uow;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.IRepositories;
using Abp.Web.Models;

namespace MQKJ.BSMP.ChineseBabies.Babies
{
    public class BabyPrestigeAppService : AbpServiceBase, IBabyPrestigeAppService
    {

        private readonly IIocResolver _iocResolve;
        private readonly IRepository<FamilyWorshipRecord> _familyWorshipRecordRepository;
        private readonly IRepository<FamilyWorshipReward> _familyWorshipRewardRepository;
        private readonly IRepository<Baby> _babyRepository;
        private readonly IRepository<FamilyCoinDepositChangeRecord, Guid> _coinRecordRepository;
        private readonly IRepository<Family> _familyRepository;
        private readonly IRepository<SystemSetting> _sysSetRepository;
        private readonly int _worshipedTimesMax,_toWorshipTimesMax;
        private readonly IRepository<Information, Guid> _infoRepository;
        public BabyPrestigeAppService(IIocResolver iocResolve,
            IRepository<Information, Guid> infoRepository,
            IRepository<FamilyWorshipRecord> familyWorshipRecordRepository,
            IRepository<FamilyWorshipReward> familyWorshipRewardRepository,
            IRepository<Family> familyRepository,
            IRepository<Baby> babyRepository,
            IRepository<SystemSetting> sysSetRepository,
            IRepository<FamilyCoinDepositChangeRecord, Guid> coinRecordRepository,
            IBabyPrestigeRepository babyPrestigeRepository)
        {
            _iocResolve = iocResolve;
            _familyWorshipRecordRepository = familyWorshipRecordRepository;
            _familyWorshipRewardRepository = familyWorshipRewardRepository;
            _familyRepository = familyRepository;
            _babyRepository = babyRepository;
            _coinRecordRepository = coinRecordRepository;
            _infoRepository = infoRepository;
            _sysSetRepository = sysSetRepository;

            if (!int.TryParse(_sysSetRepository.FirstOrDefault(s => s.Name.Equals("WorshipedTimesMax")).Value, out _worshipedTimesMax)) ThrowException("系统参数[WorshipedTimesMax]配置格式错误");
            if (!int.TryParse(_sysSetRepository.FirstOrDefault(s => s.Name.Equals("ToWorshipTimesMax")).Value, out _toWorshipTimesMax)) ThrowException("系统参数[ToWorshipTimesMax]配置格式错误");


        }
        private Func<BabyPrestigeAppService, Task<IList<Baby>>> GetAllFamilies = async (self) => await self._babyRepository.GetAllIncluding(s => s.Family).ToListAsync();
        private IQueryable<RankPrestigesItem> RankPrestigsQuery(Expression<Func<Family, bool>> predicate, int familyId)
        {
            var _worshipRecords = _familyWorshipRecordRepository.GetAll();
            return _familyRepository.GetAllIncluding(s => s.Babies).Where(predicate)
               .Select(s => new
               {
                   Age = s.Babies.Any() ? s.Babies.First().AgeString : "",
                   BabyName = s.Babies.Any() ? s.Babies.First().Name : "",
                   Popularity = s.Prestiges,
                   IsWorshipedToday = _worshipRecords.Any(w => w.FromFamilyId == familyId && w.ToFamilyId == s.Id && w.CreationTime.Date == DateTime.Now.Date),// rs.r1 != null && rs.r1.FromFamilyId == 159,
                   TimesToday = _worshipRecords.Count(t => t.ToFamilyId == s.Id && t.CreationTime.Date == DateTime.Now.Date),
                   FamilyId = s.Id,
                   PropertyTotal = s.Babies.Any() ? s.Babies.First().PropertyTotal : -1,
                   CreationTime = s.CreationTime
               })
               .OrderByDescending(f => f.Popularity)
               .ThenByDescending(f => f.PropertyTotal)
               .ThenBy(f => f.CreationTime)
               .Select((s) => new RankPrestigesItem
               {
                   Age = s.Age,
                   BabyName = s.BabyName,
                   FamilyId = s.FamilyId,
                   IsWorshipedToday = s.IsWorshipedToday,
                   Popularity = s.Popularity,
                   TimesToday = s.TimesToday
               });
        }

        /// <summary>
        /// 根据babyid查询排行榜
        /// </summary>
        /// <param name="baby"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<RankPrestigesOutput> RankPrestigesByBaby(string babyId)
        {
            var _result = RankPrestigesOutput.NewInstance();
            var  _worshipRecords = await _familyWorshipRecordRepository.GetAll()
                .Where(s => s.CreationTime.Date == System.DateTime.Now.Date)
                .ToListAsync();
            var alldata = await _babyRepository.GetAllIncluding(s => s.Family)
                     .Where(b => !b.Family.IsDeleted && b.State == BabyState.UnderAge).ToListAsync();
            var hasBaby = int.TryParse(babyId, out int baby);
            if (hasBaby) {
               if( alldata.FirstOrDefault(b => b.Id == baby) == null) ThrowException("请核对宝宝标识");
                _result.SelfInfo = alldata
                  .OrderByDescending(f => f.Family.Prestiges)
                  .ThenByDescending(s => s.PropertyTotal)
                  .ThenBy(s => s.CreationTime)
                  .Select((s, idx) => new RankPrestigesItem
                  {
                      BabyId = s.Id,
                      Age = s.AgeString,
                      BabyName = s.Name,
                      Popularity = s.Family.Prestiges,
                      IsWorshipedToday = false,
                      TimesToday = _worshipRecords.Count(t => t.ToBabyId == s.Id),
                      FamilyId = s.FamilyId,
                      RankingNumber = idx + 1,
                      Surplus = _toWorshipTimesMax - _worshipRecords.Count(t => t.FromBabyId == s.Id)
                  })
                  .Where(s => s.BabyId == baby)
                  .First();

            }        
            var p100 = alldata
                 .OrderByDescending(f => f.Family.Prestiges)
                 .ThenByDescending(s => s.PropertyTotal)
                 .ThenBy(s => s.CreationTime)
                 .Take(100)
                 .Select((s, idx) => new RankPrestigesItem
                 {
                     BabyId = s.Id,
                     Age = s.AgeString,
                     BabyName = s.Name,
                     Popularity = s.Family.Prestiges,
                     IsWorshipedToday = hasBaby ? _worshipRecords.Any(w => w.FromBabyId == _result.SelfInfo.BabyId && w.ToBabyId == s.Id):false,
                     TimesToday = _worshipRecords.Count(t => t.ToBabyId == s.Id),
                     FamilyId = s.FamilyId,
                     RankingNumber = idx + 1
                 }).ToList();
            RankPrestigesOutput.NewPager(_result, p100, p100.Count);

            _result.TimesLimit = _worshipedTimesMax;
            return _result;
        }
        public async Task<GoToWorshipOutput> GoToWorship(GoToWorshipInput input)
        {
            var _result = new GoToWorshipOutput();
            //Guid _playerId =0;
            if (!int.TryParse(input.BabyId, out int _selfBabyId)) ThrowException("请核对当前宝宝标识信息");
            if (!int.TryParse(input.WorshipedBabyId, out int _otherBabyId)) ThrowException("请核对被膜拜的宝宝标识信息");
            var validPlayerid = !Guid.TryParse(input.PlayerId, out Guid _playerId);
            if (string.IsNullOrWhiteSpace(input.PlayerId) || validPlayerid) ThrowException("请核对玩家标识信息");
           
            //当前宝宝信息
            var _currentFamilyBaby = await _babyRepository.GetAllIncluding(s => s.Family).FirstOrDefaultAsync(o => o.Id == _selfBabyId);
            if (_currentFamilyBaby == null) ThrowException("请核对当前家庭标识信息");
            if(_currentFamilyBaby.State == BabyState.Adult) ThrowException("当前宝宝已成年，膜拜失败");
            var _otherFamilyBaby = await _babyRepository.GetAllIncluding(s => s.Family).FirstOrDefaultAsync(o => o.Id == _otherBabyId);
            if (_otherFamilyBaby == null) ThrowException("请核对当前家庭标识信息");
            if(_otherFamilyBaby.State == BabyState.Adult) ThrowException("被膜拜家庭宝宝已成年，膜拜失败");
            //获取当天所有的膜拜记录
            var _worshipRecords = await _familyWorshipRecordRepository.GetAll()
                .Where(w => w.CreationTime.Date == DateTime.Now.Date)
                .Where(w => w.FromBabyId ==_currentFamilyBaby.Id  || w.ToBabyId == _otherFamilyBaby.Id)
                .ToListAsync();
            var _worshipedToday = _worshipRecords.Count(r => r.ToBabyId == _otherFamilyBaby.Id);
            //超过最大膜拜次数,操作不能进行
            if (_worshipedToday == _worshipedTimesMax) ThrowException($"每个家庭每天最多可被膜拜{_worshipedTimesMax}次,{_otherFamilyBaby.Name}的家庭今日已经不可膜拜。");
            //今日的膜拜次数已用完
            if (_worshipRecords.Count(w => w.FromBabyId == _currentFamilyBaby.Id) == _toWorshipTimesMax) ThrowException($"每天最多可膜拜{_toWorshipTimesMax}个家庭，今日膜拜次数已不足。");
            //每个家庭当前用户只能膜拜一次
            if (_worshipRecords.Any(a => a.FromBabyId == _currentFamilyBaby.Id && a.ToBabyId == _otherFamilyBaby.Id)) ThrowException("每个家庭每天只能膜拜一次,今日已膜拜。");
            //获取奖励信息
            var _worshipRewards = await _familyWorshipRewardRepository.GetAllListAsync();
            //查询当前
            var allFamilies = await GetAllFamilies(this);
            var orderedFamiliesBefore = GoToRankList(allFamilies);
            //当前家庭名次
            var _current_selfRank = orderedFamiliesBefore.Select((s1, idx) => new { s1, idx = idx + 1 }).Where(s => s.s1.Id == _selfBabyId).First(); //||s.Id ==_otherId Select((s, idx) => new { s.Id, idx = idx + 1 })
            var _current_otherRank = orderedFamiliesBefore.Select((s1, idx) => new { s1, idx = idx + 1 }).Where(s => s.s1.Id == _otherBabyId).First();
            var _worshipRewardPrestige = _worshipRewards.First(s => _current_selfRank.idx >= s.RankMax && _current_selfRank.idx <= s.RankMin && s.Type == FamilyWorshipRewardType.Prestige);
            //更新家庭声望值
            _familyRepository.Update(_otherFamilyBaby.FamilyId, f =>
            {
                f.Prestiges = _otherFamilyBaby.Family.Prestiges + _worshipRewardPrestige.Prestiges;
            });
            //奖励金币:根据之前的位置来确定金币获得多少
            var _worshipRewardCoin = _worshipRewards.First(s => _current_otherRank.idx >= s.RankMax && _current_otherRank.idx <= s.RankMin && s.Type == FamilyWorshipRewardType.COIN);
            var _randomCoins = RandomHelper.GetRandom(_worshipRewardCoin.CoinsMin, _worshipRewardCoin.CoinsMax);
            _result.Coins = _randomCoins;
            await _coinRecordRepository.InsertAsync(new FamilyCoinDepositChangeRecord
            {
                Amount = _randomCoins,
                FamilyId = _currentFamilyBaby.FamilyId,
                GetWay = CoinGetWay.SystemPresent,
                CurrentFamilyCoinDeposit = _currentFamilyBaby.Family.Deposit,

            });
            //膜拜方收到的是家庭消息
            this.PushMessages(this,
                $"孩子{ParentToString(_currentFamilyBaby.Family.FatherId == _playerId)}膜拜了{_otherFamilyBaby.Name}的家庭，获得{_randomCoins}金币",
                InformationType.Event,
                _currentFamilyBaby.Family.FatherId,
                _currentFamilyBaby.Family.MotherId,
                _currentFamilyBaby.Family.Id
                );
            _result.DepositBefore = _currentFamilyBaby.Family.Deposit;
            _result.DepositAfter = _currentFamilyBaby.Family.Deposit + _randomCoins;
            _familyRepository.Update(_currentFamilyBaby.FamilyId, f =>
            {
                f.Deposit = _currentFamilyBaby.Family.Deposit + _randomCoins;
            });         
            await _familyWorshipRecordRepository.InsertAsync(new FamilyWorshipRecord
            {
                Coins = _randomCoins,
                FromFamilyId = _currentFamilyBaby.FamilyId,
                FromBabyId =  _currentFamilyBaby.Id,
                ToFamilyId = _otherFamilyBaby.FamilyId,
                ToBabyId = _otherFamilyBaby.Id,
                Prestiges = _worshipRewardPrestige.Prestiges
            });
            return _result;
        }
        private Action<BabyPrestigeAppService, string, InformationType, Guid, Guid, int> PushMessages = (BabyPrestigeAppService self,
                string content, InformationType msgtype, Guid fatherid, Guid motherid, int familyid)
                =>
           {
               Task.Run(() =>
               {
                   self._infoRepository.Insert(
                    new Information
                    {
                        Content = content,
                        Type = msgtype,
                        ReceiverId = motherid,
                        SenderId = fatherid,
                        State = InformationState.Create,
                        SystemInformationType = (msgtype == InformationType.System ? SystemInformationType.BigBag : SystemInformationType.Default),
                        NoticeType = NoticeType.Default,
                        FamilyId = familyid
                    });
                   self._infoRepository.Insert(
                      new Information
                      {
                          Content = content,
                          Type = msgtype,
                          ReceiverId = fatherid,
                          SenderId = motherid,
                          State = InformationState.Create,
                          SystemInformationType = (msgtype == InformationType.System ? SystemInformationType.BigBag : SystemInformationType.Default),
                          NoticeType = NoticeType.Default,
                          FamilyId = familyid
                      });

               });
           };
        private Func<bool, string> ParentToString = (bool isfather) => isfather ? "爸爸" : "妈妈";
        private Action<string> ThrowException = (string str) => throw new Exception(str);
        private Func<IList<Baby>, IList<Baby>> GoToRankList = (babies) => babies.Where(b => b.State == BabyState.UnderAge &&!b.Family.IsDeleted)
                 .OrderByDescending(f => f.Family.Prestiges)
                 .ThenByDescending(f => f.PropertyTotal)
                 .ThenBy(f => f.Family.CreationTime).ToList();

    }

}