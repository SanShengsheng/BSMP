using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.TextAudio
{
   public class TextAudioBuilder
    {
        private readonly BSMPDbContext _context;

        public TextAudioBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new TextAudioCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
