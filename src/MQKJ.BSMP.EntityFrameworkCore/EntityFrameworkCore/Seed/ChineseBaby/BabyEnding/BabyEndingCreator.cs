using System.Linq;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.BonusPoints
{
    public class BabyEndingCreator
    {
        private readonly BSMPDbContext _dbContext;

        public BabyEndingCreator(BSMPDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create()
        {
            CreateBabyEnding();
        }

        public void CreateBabyEnding()
        {
            if (!_dbContext.BabyEndings.Any())
            {
                _dbContext.BabyEndings.AddRangeAsync(new ChineseBabies.BabyEnding()
                {
                
                    Name = "单身狗",
                    Image = "1.jpg",
                    StudyType = ChineseBabies.StudyType.Character,
                    Description="最后是一只单身狗"
                }, new ChineseBabies.BabyEnding()
                {
                  
                    Name = "网络喷子",
                    Image = "2.jpg",
                    StudyType = ChineseBabies.StudyType.Character,
                    Description="我的梦想是当一名“键盘侠”"
                });
            }
        }
    }
}
