using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Context;
using Infrastructure.Entity;

namespace DAL.Entities
{
    public class JobApplication : IEntity, ISoftDeleteEntity
    {
        public int Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(JobPortalDbContext.JobApplications);

        [Required]
        public int JobOfferId { get; set; }

        public virtual JobOffer JobOffer { get; set; }

        [Required]
        public JobApplicationStatus JobApplicationStatus { get; set; }

        public virtual List<QuestionAnswer> QuestionAnswers { get; set; }

        [Required]
        public int ApplicantId { get; set; }

        public virtual Applicant Applicant { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}