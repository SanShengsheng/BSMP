using MQKJ.BSMP.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Tests.Seed
{
   public class RewardConsumeBuilder
    {
        private readonly BSMPDbContext _context;

        public RewardConsumeBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new RewardConsumeCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
