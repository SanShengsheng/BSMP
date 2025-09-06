using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
   public class BabyPropPropertyAwardBuilder
    {
        private readonly BSMPDbContext _context;

        public BabyPropPropertyAwardBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new BabyPropPropertyAwardCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
