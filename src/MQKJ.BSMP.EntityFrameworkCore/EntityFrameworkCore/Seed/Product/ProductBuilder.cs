using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.Product
{
    public class ProductBuilder
    {
        private readonly BSMPDbContext _context;

        public ProductBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new ProductCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
