using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
   public class SystemMessageBuilder
    {
        private readonly BSMPDbContext _context;

        public SystemMessageBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new SystemMessageCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
