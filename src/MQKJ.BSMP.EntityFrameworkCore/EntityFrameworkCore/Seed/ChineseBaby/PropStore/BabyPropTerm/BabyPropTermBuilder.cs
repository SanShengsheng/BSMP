using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
   public class BabyPropTermBuilder
    {
        private readonly BSMPDbContext _context;

        public BabyPropTermBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new BabyPropTermCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
