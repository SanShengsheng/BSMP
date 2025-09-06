using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
   public class SystemSettingBuilder
    {
        private readonly BSMPDbContext _context;

        public SystemSettingBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new SystemSettingCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
