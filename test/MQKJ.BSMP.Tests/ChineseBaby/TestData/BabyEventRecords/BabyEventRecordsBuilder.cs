using MQKJ.BSMP.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Tests.Seed
{
   public class BabyEventRecordsBuilder
    {
        private readonly BSMPDbContext _context;

        public BabyEventRecordsBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new BabyEventRecordsCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
