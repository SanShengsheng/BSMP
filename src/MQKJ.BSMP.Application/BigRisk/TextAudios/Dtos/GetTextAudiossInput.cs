
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;

namespace MQKJ.BSMP.TextAudios.Dtos
{
    public class GetTextAudiossInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {

      public  ESceneType? Scene { get; set; }
        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }

    }
}
