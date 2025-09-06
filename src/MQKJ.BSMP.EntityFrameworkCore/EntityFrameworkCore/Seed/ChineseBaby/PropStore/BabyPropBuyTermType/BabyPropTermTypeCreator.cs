using MQKJ.BSMP.ChineseBabies;
using System.Collections.Generic;
using System.Linq;
namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class BabyPropTermTypeCreator
    {

        private readonly BSMPDbContext _context;

        public BabyPropTermTypeCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateBabyPropTermType();
        }

        private void CreateBabyPropTermType()
        {
            if (_context.BabyPropBuyTermTypes.Any())
            {
                return;
            }
            var propTypes = _context.BabyPropTypes.ToList();
            var babyPropBuyTermTypes = new List<BabyPropBuyTermType>() {
                 new BabyPropBuyTermType (){
                      Title="当前宝宝年龄",
                      Code=1,
                      Name="BabyAge",
                       Type= BabyPropBuyTermTypeType.BabyAge,
                 },
                  new BabyPropBuyTermType (){
                      Title="竞技场名次",
                      Code=2,
                      Name="ArenaRank",
                       Type= BabyPropBuyTermTypeType.ArenaRank,
                 },
                    new BabyPropBuyTermType (){
                      Title="父亲职业等级",
                      Code=4,
                      Name="FatherProfessionLevel",
                       Type= BabyPropBuyTermTypeType.FatherProfessionLevel,
                 },
                new BabyPropBuyTermType (){
                      Title="母亲职业等级",
                      Code=5,
                      Name="MotherProfessionLevel",
                       Type= BabyPropBuyTermTypeType.MotherProfessionLevel,
                 },
                    new BabyPropBuyTermType (){
                      Title="家庭房产级别",
                      Code=3,
                       BabyPropTypeId=propTypes.FirstOrDefault(s=>s.Name=="House")?.Id,
                           Name="PropLevel",
                       Type= BabyPropBuyTermTypeType.PropLevel
                 },
                    new BabyPropBuyTermType (){
                      Title="家庭佣人等级",
                          BabyPropTypeId=propTypes.FirstOrDefault(s=>s.Name=="Nanny")?.Id,
                           Name="PropLevel",
                       Type= BabyPropBuyTermTypeType.PropLevel,
                      Code=6,
                 },
                     new BabyPropBuyTermType (){
                      Title="家庭汽车等级",
                      Code=7,
                           Name="PropLevel",
                                 BabyPropTypeId=propTypes.FirstOrDefault(s=>s.Name=="Car")?.Id,
                       Type= BabyPropBuyTermTypeType.PropLevel,
                 },
                      new BabyPropBuyTermType (){
                      Title="家庭管家等级",
                      Code=8,
                           Name="PropLevel",
                                BabyPropTypeId=propTypes.FirstOrDefault(s=>s.Name=="Housekeeper")?.Id,
                       Type= BabyPropBuyTermTypeType.PropLevel,
                 },
            };
            _context.BabyPropBuyTermTypes.AddRange(babyPropBuyTermTypes);
        }
    }
}
