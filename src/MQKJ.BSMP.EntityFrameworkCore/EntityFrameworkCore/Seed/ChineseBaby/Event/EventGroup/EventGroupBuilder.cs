using MQKJ.BSMP.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
   public class EventGroupBuilder
    {
        private readonly BSMPDbContext _context;

        public EventGroupBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new EventGroupCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
