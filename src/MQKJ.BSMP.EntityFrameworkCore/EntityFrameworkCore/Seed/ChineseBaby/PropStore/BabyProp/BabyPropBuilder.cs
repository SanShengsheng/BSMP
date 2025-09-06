using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
   public class BabyPropBuilder
    {
        private readonly BSMPDbContext _context;

        public BabyPropBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new BabyPropCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
