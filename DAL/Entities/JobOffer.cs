using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Entity;

namespace DAL.Entities
{
    public class JobOffer : IEntity<int>, ISoftDeleteEntity
    {
        public int Id { get; set; }

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