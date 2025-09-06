
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


using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.WeChatPay;
using MQKJ.BSMP.WeChatPay.Dtos;
using MQKJ.BSMP.ChineseBabies.EnergyRecharges.Dtos;
using MQKJ.BSMP.ChineseBabies.Families.Dtos;
using MQKJ.BSMP.ChineseBabies.Informations.Events;
using Abp.Events.Bus;
using Abp;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// EnergyRecharge应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class EnergyRechargeAppService : BsmpApplicationServiceBase<EnergyRecharge, int, EnergyRechargeEditDto, EnergyRechargeEditDto, GetEnergyRechargesInput, EnergyRechargeListDto>, IEnergyRechargeAppService
    {

        private readonly IRepository<Player, Guid> _playerRepository;

        private readonly IRepository<Baby> _babyRepository;

        private readonly IRepository<Family> _familyRepository;

        private readonly IEnergyRechargeRecordAppService _energyRechargeRecordAppService;

        private readonly IInformationAppService _informationAppService;

        public IEventBus EventBus { get; set; }
        /// <summary>
        /// 构造函数 
        ///</summary>
        public EnergyRechargeAppService(
        IRepository<EnergyRecharge, int> entityRepository
            ,IRepository<Player, Guid> playerRepository
            , IRepository<Baby> babyRepository
            , IRepository<Family> familyRepository
            ,IEnergyRechargeRecordAppService energyRechargeRecordAppService
            , IInformationAppService informationAppService
            ) :base(entityRepository)
        {
            _playerRepository = playerRepository;

            _babyRepository = babyRepository;

            _familyRepository = familyRepository;

            _energyRechargeRecordAppService = energyRechargeRecordAppService;

            _informationAppService = informationAppService;

            EventBus = NullEventBus.Instance;
        }

        internal override IQueryable<EnergyRecharge> GetQuery(GetEnergyRechargesInput model)
        {
            return _repository.GetAll();
        }

        private const string MY_FAMILY_MESSAGE_FORMAT = "孩子的{0}花费金币{1},获得精力{2}点";

        public async Task<BuyEnergyOutput> BuyEnergy(BuyEnergyInput input)
        {
            var output = new BuyEnergyOutput();

            var entity = _repository.Get(input.Id);

            if (entity == null)
            {
                throw new UserFriendlyException("你要充值的数据没有");
            }
            else
            {
                var player = await _playerRepository.FirstOrDefaultAsync(p => p.Id == input.PlayerId);

                if (player == null)
                {
                    throw new UserFriendlyException("用户不存在");
                }
                else
                {
                    var family = await _familyRepository.GetAllIncluding(f => f.Father,m => m.Mother).FirstOrDefaultAsync(x => x.Id == input.FamilyId);

                    if (family == null)
                    {
                        throw new UserFriendlyException("该家庭不存在");
                    }
                    else
                    {
                        if (family.Deposit < entity.CointCount)
                        {
                            throw new UserFriendlyException("金币不足");
                        }
                        else
                        {
                            family.Deposit -= entity.CointCount;

                            await _familyRepository.UpdateAsync(family);

                            //更新宝宝精力
                            var baby = await _babyRepository.GetAsync(input.BabyId);

                            baby.Energy += entity.EnergyCount;

                            if (baby.Energy > 100)
                            {
                                baby.Energy = 100;
                            }

                            await _babyRepository.UpdateAsync(baby);

                            output.IsSuccess = true;

                            await _energyRechargeRecordAppService.Add(new EnergyRechargeRecordEditDto()
                            {
                                BabyId = baby.Id,
                                SourceType = SourceType.Recharge,
                                CoinAmount = entity.CointCount,
                                EnergyCount = entity.EnergyCount,
                                RechargerId = input.PlayerId,
                            });

                            var receiverId = input.PlayerId == family.FatherId ? family.MotherId : input.PlayerId;
                            var parent = input.PlayerId == family.FatherId ? "爸爸" : "妈妈";


                            await _informationAppService.Add(new InformationEditDto()
                            {
                                Content = String.Format(MY_FAMILY_MESSAGE_FORMAT, parent, entity.CointCount, entity.EnergyCount),
                                FamilyId = family.Id,
                                ReceiverId = input.PlayerId,
                                Type = InformationType.Event,
                                SenderId = input.PlayerId,
                                State = InformationState.Create,
                                NoticeType = NoticeType.Popout,
                                SystemInformationType = SystemInformationType.Recharge,
                                Remark = String.Format(MY_FAMILY_MESSAGE_FORMAT, parent, entity.CointCount, entity.EnergyCount)
                            });

                            await _informationAppService.Add(new InformationEditDto()
                            {
                                Content = String.Format(MY_FAMILY_MESSAGE_FORMAT, parent, entity.CointCount, entity.EnergyCount),
                                FamilyId = family.Id,
                                ReceiverId = receiverId,
                                Type = InformationType.Event,
                                SenderId = input.PlayerId,
                                State = InformationState.Create,
                                NoticeType = NoticeType.Popout,
                                SystemInformationType = SystemInformationType.Recharge,
                                Remark = String.Format(MY_FAMILY_MESSAGE_FORMAT, parent, entity.CointCount, entity.EnergyCount)
                            });
                        }
                    }
                }
            }

            return output;
        }

        public async Task<BuyEnergyOutput> AutoBuyEnergy(BuyEnergyInput input)
        {
            var getMaxBuyItem = await _repository.GetAll()
                .OrderByDescending(r => r.EnergyCount)
                .FirstOrDefaultAsync();

            if (getMaxBuyItem == null)
            {
                throw new AbpException($"精力表为空,无法获取");
            }

            input.Id = getMaxBuyItem.Id;

            return await BuyEnergy(input);
        }
    }
}


