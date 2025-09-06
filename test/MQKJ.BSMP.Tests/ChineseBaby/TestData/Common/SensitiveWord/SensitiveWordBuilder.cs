using MQKJ.BSMP.EntityFrameworkCore;
using MQKJ.BSMP.Tests.Seed.SensitiveWords;

namespace MQKJ.BSMP.Tests.Seed
{
    public class SensitiveWordBuilder
    {
        private readonly BSMPDbContext _context;

        public SensitiveWordBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new SensitiveWordCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
