using MQKJ.BSMP.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Tests.ChineseBaby.TestData.ProfessionCosts
{
    public class ProfessionCostBuilder
    {
        private readonly BSMPDbContext _context;

        public ProfessionCostBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new ProfessionCostCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
