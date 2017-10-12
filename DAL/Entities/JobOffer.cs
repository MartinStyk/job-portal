using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Context;
using Infrastructure.Entity;

namespace DAL.Entities
{
    public class JobOffer : IEntity, ISoftDeleteEntity
    {
        public int Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(JobPortalDbContext.JobOffers);

        [Required]
        public string Name { get; set; }

        [Required]
        public int EmployerId { get; set; }

        [Required]
        public virtual Employer Employer { get; set; }

        public String Location { get; set; }

        public string Description { get; set; }

        public virtual List<SkillTag> Skills { get; set; }

        public virtual List<Question> Questions { get; set; }

        public virtual List<JobApplication> Applications { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}