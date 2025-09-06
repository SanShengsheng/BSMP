using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class BabyPropPriceCreator
    {

        private readonly BSMPDbContext _context;

        public BabyPropPriceCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateBabyPropPrice();
            //CreateBabyPropBagPrice();
        }

        private void CreateBabyPropPrice()
        {
            if (_context.BabyPropPrices.Any())
            {
                return;
            }
            var propPrices = new List<BabyPropPrice>();
            var props = _context.BabyProps.ToList();
            var random = new Random();
            foreach (var prop in props)
            {
                for (int i = 0; i < 3; i++)
                {
                    propPrices.Add(new BabyPropPrice()
                    {
                        BabyProp = prop,
                        CurrencyType = CurrencyType.Coin,
                        Icon = prop.Code + ".jpg",
                        IsDefault = true,
                        Sort = i,
                        Price = (i + 1) * 5000,
                        Validity = i == 2 ? -1 : (i + 1) * 3600,
                        PropValue = i * 1000,
                    });
                }
            }
            _context.BabyPropPrices.AddRange(propPrices);
        }
        private void CreateBabyPropBagPrice()
        {
            //if (_context.BabyPropPrices.Any(s => s.BabyPropBagId != null))
            //{
            //    return;
            //}
            var propPrices = new List<BabyPropPrice>();
            var props = _context.BabyPropBagAndBabyProps;
            var random = new Random();
            foreach (var prop in props)
            {

                propPrices.Add(new BabyPropPrice()
                {
                    BabyPropId = prop.BabyPropId,
                    //BabyPropBagId=prop.BabyPropBagId,
                    CurrencyType = CurrencyType.Coin,
                    //Icon = prop.Code + ".jpg",
                    IsDefault = false,
                    //Sort = 1,
                    Price = random.Next(1, 20) * 10000,
                    Validity = random.Next(1, 5) * 3600,
                    PropValue = random.Next(1, 20) * 9000,
                });

            }
            _context.BabyPropPrices.AddRange(propPrices);
        }
    }
}
