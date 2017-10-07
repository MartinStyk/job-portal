using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class JobApplication : IEntity<int>, ISoftDeleteEntity
    {
        public int Id { get; set; }

        [Required]
        public int JobOfferId { get; set; }

        [Required]
        public JobOffer JobOffer { get; set; }

        [Required]
        public Status Status { get; set; }

        public virtual List<QuestionAnswer> QuestionAnswers { get; set; }

        [Required]
        public int ApplicantId { get; set; }

        [Required]
        public Applicant Applicant { get; set; }

        public DateTime? DeletedDate { get; set; }
    }

    public enum Status
    {
        Open,
        Accepted,
        Rejected
    }
}