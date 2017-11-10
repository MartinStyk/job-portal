using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class JobOfferFilterDto : FilterDtoBase
    {
        public string Name { get; set; }

        public int? EmployerId { get; set; }

        public int? SkillId { get; set; }

    }
}