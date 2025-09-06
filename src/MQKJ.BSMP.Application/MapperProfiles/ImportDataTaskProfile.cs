using AutoMapper;
using MQKJ.BSMP.ChineseBabies;
namespace MQKJ.BSMP.MapperProfiles
{
    public class ImportDataTaskProfile : Profile
    {
        public ImportDataTaskProfile()
        {
            CreateMap<ImportDataRecord, ImportDataTaskOutput>().AfterMap((src, dest) => {
                dest.FileTypeDescription = EnumHelper.EnumHelper.GetDescription(dest.FileDataType);
            });
        }
    }
}
