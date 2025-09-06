using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.Tag
{
    public class TagBuilder
    {
        private readonly BSMPDbContext _context;

        public TagBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new TagCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
