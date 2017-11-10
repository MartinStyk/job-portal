using BusinessLayer.DataTransferObjects.Common;
using DAL.Entities;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class JobApplicationFilterDto : FilterDtoBase
    {
        public int? JobOfferId { get; set; }

        public JobApplicationStatus? JobApplicationStatus { get; set; }

        public int? ApplicantId { get; set; }
    }
}