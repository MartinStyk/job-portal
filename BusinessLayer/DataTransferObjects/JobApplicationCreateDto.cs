using System.Collections.Generic;
using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects
{
    public class JobApplicationCreateDto : DtoBase
    {
        public int JobOfferId { get; set; }

        public JobOfferDto JobOffer { get; set; }
        
        public virtual List<QuestionAnswerDto> QuestionAnswers { get; set; }

        public ApplicantDto Applicant { get; set; }

        public int ApplicantId { get; set; }

    }
}