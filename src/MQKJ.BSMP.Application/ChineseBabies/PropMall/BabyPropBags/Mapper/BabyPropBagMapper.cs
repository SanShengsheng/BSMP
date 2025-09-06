
using AutoMapper;
using MQKJ.BSMP.ChineseBabies.PropMall;
using MQKJ.BSMP.ChineseBabies.PropMall.Dtos;
using System.Configuration;

namespace MQKJ.BSMP.ChineseBabies.PropMall.Mapper
{

    /// <summary>
    /// 配置BabyPropBag的AutoMapper
    /// </summary>
    internal class BabyPropBagMapper : Profile
    {
        public   BabyPropBagMapper()
        {
           CreateMap <BabyPropBag,BabyPropBagListDto>();
           CreateMap <BabyPropBagListDto,BabyPropBag>();

           CreateMap <BabyPropBagEditDto,BabyPropBag>();
           CreateMap <BabyPropBag,BabyPropBagEditDto>();


            CreateMap<BabyPropBag, GetPropBagLastestOutputBasicInfo>();

        }
    }
}
