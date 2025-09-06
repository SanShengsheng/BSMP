using Abp.Application.Services;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies.PropMall.BabyPropTerms.Dtos;
using MQKJ.BSMP.ChineseBabies.PropMall.Props.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQKJ.BSMP.Utils;
using MQKJ.BSMP.ChineseBabies.Backpack;

namespace MQKJ.BSMP.ChineseBabies.PropMall
{
    public class BabyPropTermApplicationService : IBabyPropTermApplicationService
    {
        private readonly IRepository<Baby, int> _babyRepository;
        private readonly IRepository<PlayerProfession, int> _playerProfessionRepository;
        private readonly IRepository<Family, int> _familyRepository;
        private readonly IRepository<BabyFamilyAsset, Guid> _babyFamilyAssetRepository;

        public BabyPropTermApplicationService(
            IRepository<Baby, int> babyRepository
            , IRepository<PlayerProfession, int> playerProfessionRepository
           , IRepository<Family, int> familyRepository
            , IRepository<BabyFamilyAsset, Guid> babyFamilyAssetRepository
            )
        {
            _babyRepository = babyRepository;
            _playerProfessionRepository = playerProfessionRepository;
            _familyRepository = familyRepository;
            _babyFamilyAssetRepository = babyFamilyAssetRepository;
        }



        public async Task<bool> ValideBabyPropTermSatisfyHandle(BabyPropBuyTermIsSatisfyInput input)
        {
            var res = false;
            switch (input.BabyPropBuyTermType.Name)
            {
                case "BabyAge":
                    res = await AgeTermValidate(input);
                    break;
                //case "ArenaRank":
                //    res = await ArenaRankTermValidate(input);
                //    break;
                case "FatherProfessionLevel":
                    res = await ProfessionLevelTermValidate(input, "dad");
                    break;
                case "MotherProfessionLevel":
                    res = await ProfessionLevelTermValidate(input, "mom");
                    break;
                case "PropLevel":
                    res = PropLevelTermValidate(input);
                    break;
            }
            return res;
        }
        /// <summary>
        /// 道具级别条件验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool PropLevelTermValidate(BabyPropBuyTermIsSatisfyInput input)
        {
            // 获取家庭资产中未过期的本类装备级别
            return _babyFamilyAssetRepository.GetAllIncluding(s => s.BabyProp)
                    .Where(s => s.BabyProp != null && s.BabyProp.BabyPropTypeId == input.BabyPropBuyTermType.BabyPropTypeId && (s.ExpiredDateTime >= DateTime.UtcNow || s.ExpiredDateTime == null) && (s.OwnId == input.BabyId || (s.BabyProp.IsInheritAble && s.FamilyId == input.FamilyId)))
                    .WhereIf(input.Term.MinValue.HasValue, s => Convert.ToInt32(s.BabyProp.Level) >= input.Term.MinValue)
                    .WhereIf(input.Term.MaxValue.HasValue && input.Term.MaxValue != 0, s => Convert.ToInt32(s.BabyProp.Level) <= input.Term.MaxValue)
                    .Any();
        }


        /// <summary>
        /// 父母职业等级校验
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parentName"></param>
        /// <returns></returns>

        public async Task<bool> ProfessionLevelTermValidate(BabyPropBuyTermIsSatisfyInput input, string parentName)
        {
            var family = await _familyRepository.GetAsync(input.FamilyId);
            var profession = await _playerProfessionRepository.GetAllIncluding(s => s.Profession)
                     .WhereIf(parentName == "mom", s => s.PlayerId == family.MotherId)
                     .WhereIf(parentName == "dad", s => s.PlayerId == family.FatherId).AsNoTracking()
                     .FirstOrDefaultAsync(s => s.IsCurrent && s.FamilyId == input.FamilyId);
            return (input.Term.MinValue.HasValue && profession.Profession.Grade >= input.Term.MinValue) || (input.Term.MaxValue.HasValue && profession.Profession.Grade <= input.Term.MaxValue);
        }
        ///// <summary>
        ///// 竞技场排名校验
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //private async Task<bool> ArenaRankTermValidate(BabyPropBuyTermIsSatisfyInput input)
        //{
        //    return true;
        //}
        /// <summary>
        /// 年龄条件验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AgeTermValidate(BabyPropBuyTermIsSatisfyInput input)
        {
            var baby = await _babyRepository.GetAsync(input.BabyId);
            return baby.AgeDouble >= input.Term?.MinValue && baby.AgeDouble <= input.Term?.MaxValue;
        }


    }
}
