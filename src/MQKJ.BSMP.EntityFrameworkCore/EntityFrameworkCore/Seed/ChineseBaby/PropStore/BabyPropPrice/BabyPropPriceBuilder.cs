using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
   public class BabyPropPriceBuilder
    {
        private readonly BSMPDbContext _context;

        public BabyPropPriceBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new BabyPropPriceCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
