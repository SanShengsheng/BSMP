using Abp.Domain.Repositories;
using MQKJ.BSMP.ChineseBabies;
using System;
using Xunit;
using System.Linq;
using Abp.Domain.Uow;
using System.Threading.Tasks;
using Shouldly;
using MQKJ.BSMP.ChineseBabies.Babies;
using MQKJ.BSMP.ChineseBabies.Prestiges;
using Microsoft.EntityFrameworkCore;

namespace MQKJ.BSMP.Tests.ChineseBaby
{
    public class PrestigeAppService_Tests : BSMPTestBase
    {
        private readonly IBabyPrestigeAppService _babyPrestigeAppService;
        private readonly IRepository<ChineseBabies.Baby> _babyRepo;
        private readonly IUnitOfWorkManager _uowManager;
        public PrestigeAppService_Tests()
        {
            _babyRepo = Resolve<IRepository<ChineseBabies.Baby>>();
            _babyPrestigeAppService = Resolve<IBabyPrestigeAppService>();
            _uowManager = Resolve<IUnitOfWorkManager>();
            UsingDbContext(ctx =>
            {
                ctx.Babies.RemoveRange(ctx.Babies);
                ctx.Families.RemoveRange(ctx.Families);
            });
            UsingDbContext(ctx =>
            {
                var _allplayers = ctx.Players.ToList();
                var player1 = _allplayers.Last();
                var player2 = ctx.Players.First();
                var player3 = _allplayers[1];
                ctx.Families.AddRange(new Family[] {
                    new Family{
                        Prestiges =1,
                        CreationTime =DateTime.Now,
                        MotherId =player1.Id,
                        FatherId = player2.Id,
                        Babies = new MQKJ.BSMP.ChineseBabies.Baby[]{
                             new ChineseBabies.Baby{
                                 State = BabyState.UnderAge,
                                 Name ="baby1"
                             },
                             new ChineseBabies.Baby{
                                  State = BabyState.Adult,
                                  Name ="adult"
                             }
                        },
                        IsDeleted = false
                    },
                    new Family{
                        Prestiges =2,
                        CreationTime =System.DateTime.Now,
                        MotherId =player1.Id,
                        FatherId = player3.Id,
                        Babies = new MQKJ.BSMP.ChineseBabies.Baby[]{
                             new ChineseBabies.Baby{
                                 State = BabyState.UnderAge,
                                 Name ="baby2"
                             },
                             new ChineseBabies.Baby{
                                  State = BabyState.Adult,
                                  Name ="adult2"
                             }
                        },
                        IsDeleted = false
                    },
                      new Family{
                        Prestiges =3,
                        CreationTime =System.DateTime.Now,
                        MotherId =player2.Id,
                        FatherId =player3.Id ,
                        Babies = new MQKJ.BSMP.ChineseBabies.Baby[]{
                             new ChineseBabies.Baby{
                                 State = BabyState.UnderAge,
                                 Name ="baby2"
                             },
                             new ChineseBabies.Baby{
                                  State = BabyState.Adult,
                                  Name ="adult2"
                             }
                        },
                        IsDeleted = false
                    }
                });
            });
        }
        [Fact]
        public async Task Ranks_Test_Init_data()
        {
            await _uowManager.Begin().AsDispose(async (_uowHandler) =>
            {
                var _result = await _babyRepo.GetAllIncluding(s => s.Family).ToListAsync();
                _result.ShouldNotBeNull();
                _result.Count.ShouldBe(6);
                var _familyRepo = Resolve<IRepository<Family>>();
                _familyRepo.ShouldNotBeNull();
                _familyRepo.Count().ShouldBe(3);
                (_uowHandler as IUnitOfWorkCompleteHandle).Complete();
            });
        }
        [Fact]
        public async Task Ranks_OrderByPrestigesDesc_ThenByDimensionsDesc_ThenByFamilyCreationTimeAsc()
        {
            _babyRepo.ShouldNotBeNull();
            _uowManager.ShouldNotBeNull();
            await _uowManager.Begin().AsDispose(async (_uowHandler) =>
            {
                var sortdata = _babyRepo.GetAllIncluding(s => s.Family)
                    .Where(s => s.State == BabyState.UnderAge && !s.Family.IsDeleted)
                    .OrderByDescending(s => s.Family.Prestiges)
                    .ThenByDescending(s => s.PropertyTotal)
                    .ThenBy(s => s.CreationTime)
                    .ToList();
                var _firstBaby = sortdata.First();
                var _lastBaby = sortdata.Last();
                var _depositBefore = _firstBaby.Family.Deposit;
                var _sortlist = await _babyPrestigeAppService.RankPrestigesByBaby(_firstBaby.Id.ToString());
                _sortlist.ShouldNotBeNull();
                _sortlist.SelfInfo.ShouldNotBeNull();
                _sortlist.SelfInfo.BabyId.ShouldBe(_firstBaby.Id);
                _sortlist.PagedResultDto.Items.ShouldNotBeNull();
                _sortlist.PagedResultDto.Items.Count.ShouldBe(sortdata.Count);
                _sortlist.PagedResultDto.TotalCount.ShouldBe(sortdata.Count);

                for (int i = 0; i < _sortlist.PagedResultDto.Items.Count; i++)
                {
                    _sortlist.PagedResultDto.Items[i].BabyId.ShouldBe(sortdata[i].Id);
                }
                 (_uowHandler as IUnitOfWorkCompleteHandle).Complete();
            });
        }
        [Fact]
        public async Task Ranks_Worshiped_Then_Sort()
        {
            var _familyCoinsRecordRepo = Resolve<IRepository<FamilyCoinDepositChangeRecord, Guid>>();
            var _familyWorshipRecodeRepo = Resolve<IRepository<FamilyWorshipRecord>>();
            var _infoRepo = Resolve<IRepository<Information, Guid>>();
            _familyCoinsRecordRepo.ShouldNotBeNull();
            _babyRepo.ShouldNotBeNull();
            _uowManager.ShouldNotBeNull();
            _infoRepo.ShouldNotBeNull();
            _infoRepo.Count().ShouldBe(0);
            _familyWorshipRecodeRepo.Count().ShouldBe(0);
            _familyCoinsRecordRepo.Count().ShouldBe(0);
            await _uowManager.Begin().AsDispose(async (_uowHandler) =>
            {
                await _uowManager.Current.DisableFilter().AsDispose(async (IDisposable dispose) =>
                {
                    var _babiesData = _babyRepo.GetAllIncluding(s => s.Family).ToList();
                    var sortdata = _babiesData.Where(s => s.State == BabyState.UnderAge)
                        .OrderByDescending(s => s.Family.Prestiges)
                        .ThenByDescending(s => s.PropertyTotal)
                        .ThenBy(s => s.CreationTime)
                        .ToList();
                    var _firstBaby = sortdata.First();
                    var _lastBaby = sortdata.Last();
                    var _depositBefore = _firstBaby.Family.Deposit;
                    var _sortlistBefore = await _babyPrestigeAppService.RankPrestigesByBaby(_firstBaby.Id.ToString());
                    var _worshipResult = await _babyPrestigeAppService.GoToWorship(new ChineseBabies.Babies.Dtos.PrestigeDtos.GoToWorshipInput
                    {
                        BabyId = _firstBaby.Id.ToString(),
                        WorshipedBabyId = _lastBaby.Id.ToString(),
                        PlayerId = _firstBaby.Family.MotherId.ToString()
                    });
                    _uowManager.Current.SaveChanges();

                    _familyCoinsRecordRepo.Count().ShouldBe(1);
                    _worshipResult.ShouldSatisfyAllConditions("worshiped failed!", () =>
                    {
                        _worshipResult.ShouldNotBeNull();
                        _worshipResult.DepositBefore.ShouldBe(_depositBefore);
                        _worshipResult.DepositAfter.ShouldBe(_depositBefore + _worshipResult.Coins);
                    });
                    (from item in _infoRepo.GetAll() where item.Type == InformationType.System && item.SystemInformationType == SystemInformationType.BigBag select item).Count().ShouldBe(2);
                    (from item in _infoRepo.GetAll() where item.Type == InformationType.Event select item).Count().ShouldBe(2);
                    var _sortlistAfter = await _babyPrestigeAppService.RankPrestigesByBaby(_firstBaby.Id.ToString());

                    _worshipResult.ShouldSatisfyAllConditions("ranking by conditions failed!", () =>
                    {
                        _sortlistAfter.ShouldNotBeNull();
                        _sortlistAfter.SelfInfo.ShouldNotBeNull();
                        _sortlistAfter.PagedResultDto.ShouldNotBeNull();
                    });
                    _sortlistAfter.PagedResultDto.Items.First().BabyId.ShouldBe(_lastBaby.Id);

                    _familyWorshipRecodeRepo.Count().ShouldBe(1);

                    _familyWorshipRecodeRepo.GetAll()
                        .Where(w => w.FromFamilyId == _firstBaby.Family.Id && w.ToFamilyId == _lastBaby.Family.Id)
                        .Count()
                        .ShouldBe(1);
                });
                (_uowHandler as IUnitOfWorkCompleteHandle).Complete();
            });
        }
    }
    internal static class UnitOfWorkCompleteHandleExtension
    {
        public static Task AsDispose(this IDisposable _uowHandler, Func<IDisposable, Task> action)
        {
            using (_uowHandler)
            {
                return action(_uowHandler);
            }
        }
    }
}
