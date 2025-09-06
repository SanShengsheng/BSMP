using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.ChineseBaby.Athletics
{
    public class SeasonManagementBuilder
    {
        private readonly BSMPDbContext _context;

        public SeasonManagementBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new SeasonManagementCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
