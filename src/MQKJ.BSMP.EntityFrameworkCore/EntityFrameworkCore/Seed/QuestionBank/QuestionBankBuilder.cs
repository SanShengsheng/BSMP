namespace MQKJ.BSMP.EntityFrameworkCore.Seed.QuestionBank
{
    public class QuestionBankBuilder
    {
        private readonly BSMPDbContext _context;

        public QuestionBankBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            //new DramaCreator(_context).Create();
            //new DramaQuestionLibraryCreator(_context).Create();
            //new QuestionBankRuleCreator(_context).Create();
            //new QuestionBankCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
