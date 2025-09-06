using MQKJ.BSMP.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Tests.Seed
{
   public class FamilyBuilder
    {
        private readonly BSMPDbContext _context;

        public FamilyBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new FamilyCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
