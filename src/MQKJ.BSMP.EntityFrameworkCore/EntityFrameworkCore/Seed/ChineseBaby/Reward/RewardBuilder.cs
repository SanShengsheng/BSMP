using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
   public class RewardBuilder
    {
        private readonly BSMPDbContext _context;

        public RewardBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new RewardCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
