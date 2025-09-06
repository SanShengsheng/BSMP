using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.PropMall;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class BabyPropBagAndBabyPropCreator
    {

        private readonly BSMPDbContext _context;

        public BabyPropBagAndBabyPropCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            //if (_context.BabyPropBagAndBabyProps.Any())
            //{
            //    return;
            //}
            //var babyPropBagAndBabyProps = new List<BabyPropBagAndBabyProp>();
            //var propTypeDics = _context.BabyProps.Include(s => s.BabyPropType).Include(s => s.Prices)
            //              .GroupBy(s => s.BabyPropType.Name).ToDictionary(d => d.Key, g => g.ToList());
            //var propBags = _context.BabyPropBags;
            //var ran = new Random();
            //foreach (var item in propBags)
            //{
            //    for (int i = 0; i < propTypeDics.Count; i++)
            //    {
            //        var prop = propTypeDics.ElementAt(i).Value.ElementAt(ran.Next(1, 10));
            //        babyPropBagAndBabyProps.Add(new BabyPropBagAndBabyProp()
            //        {
            //            BabyPropBagId = item.Id,
            //            BabyPropId = prop.Id,
            //            BabyPropPriceId = prop.Prices.FirstOrDefault().Id
            //        });
            //    }
            //}
            //_context.BabyPropBagAndBabyProps.AddRange(babyPropBagAndBabyProps);
        }
    }
}
