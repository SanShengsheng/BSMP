using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.PropMall;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class BabyPropPropertyAwardCreator
    {

        private readonly BSMPDbContext _context;

        public BabyPropPropertyAwardCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateBabyPropPropertyAward();
        }

        private void CreateBabyPropPropertyAward()
        {
            if (_context.BabyPropPropertyAwards.Any())
            {
                return;
            }
            var propAwards = new List<BabyPropPropertyAward>();
            var random = new Random();
            //var i = 0;
            for (int i = 0; i < 100; i++)
            {
                //++i;
                propAwards.Add(new BabyPropPropertyAward()
                {
                    EventAdditionType = EventAdditionType.GrowUpAndStudy,
                    Physique = random.Next(100, 300),
                    Charm = random.Next(100, 300),
                    EmotionQuotient = random.Next(100, 300),
                    Imagine = random.Next(100, 300),
                    Intelligence = random.Next(100, 300),
                    WillPower = random.Next(100, 300),
                    Code = i,
                });
            }

            _context.BabyPropPropertyAwards.AddRange(propAwards);
        }
    }
}
