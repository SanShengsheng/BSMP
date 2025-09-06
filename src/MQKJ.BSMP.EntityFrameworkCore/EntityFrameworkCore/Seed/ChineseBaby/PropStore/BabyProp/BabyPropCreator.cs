using MQKJ.BSMP.ChineseBabies;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class BabyPropCreator
    {

        private readonly BSMPDbContext _context;

        public BabyPropCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateBabyProp();
            UpdateBabyPropPropertyAddition();
        }

        private void UpdateBabyPropPropertyAddition()
        {
            if (_context.BabyProps.Any(s => s.BabyPropPropertyAwardId != null))
            {
                return;
            }
            var babyPropPropertyAwards = _context.BabyPropPropertyAwards.ToList();
            var babyProps = new List<BabyProp>();

            _context.BabyProps.ToList().ForEach(s =>
            {
                s.BabyPropPropertyAwardId = babyPropPropertyAwards.Find(a => a.Code == s.Code)?.Id;
                babyProps.Add(s);
            });
            _context.BabyProps.UpdateRange(babyProps);

        }

        private void CreateBabyProp()
        {
            //TODO:
            if (_context.BabyProps.Any())
            {
                return;
            }
            var babyProps = new List<BabyProp>();
            var propTypes = _context.BabyPropTypes.Where(s => true).ToList();
            var babyPropPropertyAwards = _context.BabyPropPropertyAwards.ToList();
            var k = 1;
            foreach (var item in propTypes)
            {
                var level = 1;
                for (int i = 1; i <= 12; i++)
                {
                    var prop = new BabyProp()
                    {
                        BabyPropTypeId = item.Id,
                        Code = k,
                        CoverImg = k + ".jpg",
                        Gender = item.Title == "皮肤" ? (i >= 6 ? ((Gender?)Gender.Male) : (Gender?)Gender.Female) : null,
                        GetWay = i >= 10 ? GetWay.Arena : GetWay.Store,
                        IsAfterBuyPlayMarquees = true,
                        IsDefault = true,
                        IsInheritAble = item.Title != "皮肤",
                        IsNewProp = true,
                        Level = (PropLevel)level,
                        MaxPurchasesNumber = 1,
                        Title = item.Title + k + "号",
                        BabyPropPropertyAwardId = babyPropPropertyAwards.Count >= i ? babyPropPropertyAwards[i]?.Id : null,
                        TriggerShowPropCode = i==12 ? null : (int?)k + 1,
                    };
                    babyProps.Add(prop);
                    k += 1;
                    level = level == 4 ? 1 : level + 1;
                }
            };
            _context.BabyProps.AddRange(babyProps);
        }
    }
}
