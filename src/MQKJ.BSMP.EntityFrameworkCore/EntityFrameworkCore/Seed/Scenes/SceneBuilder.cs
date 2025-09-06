using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.Scenes
{
    public class SceneBuilder
    {
        private readonly BSMPDbContext _context;

        public SceneBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new SceneCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
