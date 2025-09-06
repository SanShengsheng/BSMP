using MQKJ.BSMP.Common.SensitiveWords;
using MQKJ.BSMP.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MQKJ.BSMP.Tests.Seed.SensitiveWords
{
    public class SensitiveWordCreator
    {
        private readonly BSMPDbContext _context;

        public SensitiveWordCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateSensitiveWord();
        }

        private void CreateSensitiveWord()
        {
            var sensitiveWords = _context.SensitiveWords.Count();
            if (sensitiveWords <= 0)
            {
                var sensitiveWordList = new List<SensitiveWord>();
                //try
                //{
                //    var path = Directory.GetCurrentDirectory();
                //    var txt = "";
                //    using (StreamReader sr = new StreamReader(path + "/ChineseBaby/TestData/Common/SensitiveWord/SensitiveWords.txt", Encoding.UTF8))
                //    {
                //        txt =  sr.ReadToEnd();
                //    }
                //    var test = txt.Split('\n');
                //    for (int i = 0; i < test.Length; i++)
                //    {
                //        sensitiveWordList.Add(new SensitiveWord() { Content = test[i] });
                //    }
                //    await _context.SensitiveWords.AddRangeAsync(sensitiveWordList);
                //}
                //catch (System.Exception ex)
                //{
                //    throw ex;
                //}

            }

        }
    }
}
