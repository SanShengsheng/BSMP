using MQKJ.BSMP.LoveCards.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.Product
{
    public class ProductCreator
    {
        private readonly BSMPDbContext _context;

        public ProductCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            if (_context.Products.FirstOrDefault(x => x.Name == ProductStaticName.LoveCardProductName) == null)
            {
                _context.Products.Add(new Products.Product()
                {
                    State = 1,
                    Name =ProductStaticName.LoveCardProductName,
                    Price = 10
                });
                _context.SaveChanges();
            }

        }
    }
}
