using MQKJ.BSMP.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Tests.Seed
{
   public class BabyBuilder
    {
        private readonly BSMPDbContext _context;

        public BabyBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new BabyCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
