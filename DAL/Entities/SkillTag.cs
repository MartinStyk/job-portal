using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Context;
using Infrastructure.Entity;

namespace DAL.Entities
{
    public class SkillTag : IEntity, ISoftDeleteEntity
    {
        public int Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(JobPortalDbContext.SkillTags);

        [Required]
        public string Name { get; set; }

        public virtual List<JobOffer> JobOffers { get; set; }

        public virtual List<User> Users { get; set; }

        public DateTime? DeletedDate { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}