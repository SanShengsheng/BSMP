using MQKJ.BSMP.EntityFrameworkCore.Seed.SensitiveWords;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.Scenes
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
