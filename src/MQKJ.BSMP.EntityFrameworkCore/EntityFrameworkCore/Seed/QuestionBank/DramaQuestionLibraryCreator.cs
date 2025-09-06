using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MQKJ.BSMP.DramaQuestionLibraryTypes;
using MQKJ.BSMP.Dramas;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.QuestionBank
{
    public class DramaQuestionLibraryCreator
    {
        private readonly BSMPDbContext _context;

        public DramaQuestionLibraryCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDramaQuestionLibrary();
        }

        public void CreateDramaQuestionLibrary()
        {
            var dramas = _context.Dramas.Include(d => d.DramaQuestionLibrarys);
            if (dramas.Count()>=16)
            {
                return;
            }
            var DramaQuestionLibrarys = new List<DramaQuestionLibrary>();
            var code = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            foreach (var drama in dramas)
            {
                if (drama.DramaType == DramaTypeEnum.正常)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (_context.DramaQuestionLibrarys.IgnoreQueryFilters().FirstOrDefault(q => q.Code == drama.Code + "_" + code[j]) == null)
                        {
                            DramaQuestionLibrarys.Add(new DramaQuestionLibrary { Code = drama.Code + "_" + code[j], DramaId = drama.Id });
                        }
                    }
                }
                else if (drama.DramaType == DramaTypeEnum.新手)
                {
                    if (_context.DramaQuestionLibrarys.IgnoreQueryFilters().FirstOrDefault(q => q.Code == drama.Code + "_A") == null)
                    {
                        DramaQuestionLibrarys.Add(new DramaQuestionLibrary { Code = drama.Code + "_A", DramaId = drama.Id });
                    }

                }
            }
            _context.DramaQuestionLibrarys.AddRangeAsync(DramaQuestionLibrarys);

        }
    }
}
