using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.ChineseBaby.FamilyWorships
{
    public class WorshipRewardsBuilder
    {
        private readonly BSMPDbContext _context;

        public WorshipRewardsBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new WorshipRewardsCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
