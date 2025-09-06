using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.PropMall;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class BabyPropBagCreator
    {

        private readonly BSMPDbContext _context;

        public BabyPropBagCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreatePropBag();
        }

        private void CreatePropBag()
        {
            if (_context.BabyPropBags.Any())
            {
                return;
            }
            var propBags = CreatePropBagFucn(Gender.Male).Union(CreatePropBagFucn(Gender.Female));
            _context.BabyPropBags.AddRange(propBags);
        }
        /// <summary>
        /// 创建男性大礼包
        /// </summary>
        private List<BabyPropBag> CreatePropBagFucn(Gender gender)
        {
            var babyPropBags = new List<BabyPropBag>();
          
            var ran = new Random();
            for (int i = 0; i < 3; i++)
            {
                babyPropBags.Add(new BabyPropBag()
                {
                    //BabyPropId = prop.Id,
                    Code = i,
                    Gender = gender,
                    CurrencyCount = ran.Next(1, 100000),
                    CurrencyType = CurrencyTypeEnum.GoldCoin,
                    Order = i,
                    Img = i + ".jpg",
                    PriceGoldCoin = (i + 1) * 10000,
                    PriceRMB = 0.01M,
                    Description = "这是一个有味道的大礼包" + i,
                    Title = "大礼包" + i,
                });

            }
            return babyPropBags;
        }
    }
}
