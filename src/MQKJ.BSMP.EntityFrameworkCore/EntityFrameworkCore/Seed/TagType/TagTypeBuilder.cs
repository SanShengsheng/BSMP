using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.TagType
{
    public class TagTypeBuilder
    {
        private readonly BSMPDbContext _context;

        public TagTypeBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new TagTypeCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
