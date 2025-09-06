
using AutoMapper;
using MQKJ.BSMP.LoveCards;
using MQKJ.BSMP.LoveCards.Dtos;

namespace MQKJ.BSMP.LoveCards.Mapper
{

	/// <summary>
    /// 配置LoveCard的AutoMapper
    /// </summary>
	internal static class LoveCardMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LoveCard,LoveCardListDto>();
            configuration.CreateMap <LoveCardListDto,LoveCard>();

            configuration.CreateMap <LoveCardEditDto,LoveCard>();
            configuration.CreateMap <LoveCard,LoveCardEditDto>();

        }
	}
}
