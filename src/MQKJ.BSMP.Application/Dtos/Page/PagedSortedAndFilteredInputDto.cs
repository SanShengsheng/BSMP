using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MQKJ.BSMP.Dtos
{
    public class PagedSortedAndFilteredInputDto : IPagedResultRequest, ISortedResultRequest
    {
        [Range(0, 1000)]
        public int MaxResultCount { get; set; } = 10;
    

        public int SkipCount { get; set; } = 0;

        public string Sorting { get; set; } = "CreationTime desc";
        //public string Sorting { get; set; }

        public virtual int PageIndex { get; set; }
        public virtual int PageSize { get; set; }
    }
}
