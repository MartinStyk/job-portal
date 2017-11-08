using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessLayer.DataTransferObjects.Common;
using DAL.Entities;

namespace BusinessLayer.DataTransferObjects
{
    public class JobApplicationDto : DtoBase
    {
        public int JobOfferId { get; set; }

        public JobOfferDto JobOffer { get; set; }

        [Required]
        public JobApplicationStatus JobApplicationStatus { get; set; }

        public virtual List<QuestionAnswerDto> QuestionAnswers { get; set; }

        public ApplicantDto Applicant { get; set; }

        public int ApplicantId { get; set; }

    }
}