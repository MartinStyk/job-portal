using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class JobApplication : IEntity<int>, ISoftDeleteEntity
    {
        public int Id { get; set; }

        [Required]
        public int JobOfferId { get; set; }

        [Required]
        public virtual JobOffer JobOffer { get; set; }

        [Required]
        public JobApplicationStatus JobApplicationStatus { get; set; }

        public virtual List<QuestionAnswer> QuestionAnswers { get; set; }

        [Required]
        public int ApplicantId { get; set; }

        [Required]
        public virtual Applicant Applicant { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}