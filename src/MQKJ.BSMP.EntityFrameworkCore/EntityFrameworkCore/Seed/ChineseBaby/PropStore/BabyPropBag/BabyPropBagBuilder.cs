using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
   public class BabyPropBagBuilder
    {
        private readonly BSMPDbContext _context;

        public BabyPropBagBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new BabyPropBagCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
