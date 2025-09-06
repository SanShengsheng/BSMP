using AutoMapper;
using MQKJ.BSMP.BonusPoints;

namespace MQKJ.BSMP.BonusPoints.Dtos
{

	/// <summary>
	/// 配置BonusPoint的AutoMapper
	/// </summary>
	internal static class CustomerBonusPointMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <BonusPoint, BonusPointListDto>();
            configuration.CreateMap<BonusPointEditDto, BonusPoint>();

            //自定义映射
            var map = configuration.CreateMap<BonusPointEditDto, BonusPoint>();
            map.ForMember(dto => dto.EventName, option => option.Ignore());
            map.ForMember(dto => dto.EventDescription, option => option.Ignore());


            //// custom codes 

            //// custom codes end

        }
    }
}