using MQKJ.BSMP.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Tests.Seed
{
   public class BabyEventBuilder
    {
        private readonly BSMPDbContext _context;

        public BabyEventBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new BabyEventCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
