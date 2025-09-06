using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.Common.WechatMerchants
{
    public class WechatMerchantBuilder
    {
        private readonly BSMPDbContext _context;

        public WechatMerchantBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new WechatMerchantCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
