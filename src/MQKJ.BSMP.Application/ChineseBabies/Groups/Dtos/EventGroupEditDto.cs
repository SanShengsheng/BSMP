using Abp.AutoMapper;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapTo(typeof(EventGroup))]
    public class EventGroupEditDto : IEditModel<EventGroup, int>, IAddModel<EventGroup, int>
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
		/// <summary>
		/// PrevGroupId
		/// </summary>
		public int? PrevGroupId { get; set; }



		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; }




    }
}