using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
   public class BabyPropTermTypeBuilder
    {
        private readonly BSMPDbContext _context;

        public BabyPropTermTypeBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new BabyPropTermTypeCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
