using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.ChineseBaby.Athletics.AthleticsRewards
{
    public class AthleticsRewardBuilder
    {
        private readonly BSMPDbContext _context;

        public AthleticsRewardBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new AthleticsRewardCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
