using MQKJ.BSMP.ChineseBabies;
using System.Collections.Generic;
using System.Linq;
namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class BabyPropTypeCreator
    {

        private readonly BSMPDbContext _context;

        public BabyPropTypeCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateBabyPropType();
        }

        private void CreateBabyPropType()
        {
            if (_context.BabyPropTypes.Any())
            {
                return;
            }
            var propTypes = new List<BabyPropType>() {
                 new BabyPropType (){
                     Title="房子",
                      Sort=1,
                      Code=1,
                      Img="1.jpg",
                      Name="House",
                      EquipmentAbleObject= EquipmentAbleObject.Family,
                 },
                   new BabyPropType (){
                     Title="车子",
                      Sort=2,
                        Code=2,
                      Img="2.jpg",
                      Name="Car",
                      EquipmentAbleObject= EquipmentAbleObject.Family,
                 },
                     new BabyPropType (){
                     Title="管家",
                      Sort=3,
                        Code=3,
                      Img="3.jpg",
                            Name="Housekeeper",
                      EquipmentAbleObject= EquipmentAbleObject.Family,
                 },
                       new BabyPropType (){
                     Title="保姆",
                      Sort=4,
                        Code=4,
                      Img="4.jpg",
                      Name="Nanny",
                      EquipmentAbleObject= EquipmentAbleObject.Family,
                 },
                         new BabyPropType (){
                     Title="皮肤",
                      Sort=5,
                        Code=5,
                      Img="5.jpg",
                      Name="Skin",
                      EquipmentAbleObject= EquipmentAbleObject.Baby,
                 }
            };
             _context.BabyPropTypes.AddRange(propTypes);
    }
}
}
