using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
   public class BabyPropBagAndBabyPropBuilder
    {
        private readonly BSMPDbContext _context;

        public BabyPropBagAndBabyPropBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new BabyPropBagAndBabyPropCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
