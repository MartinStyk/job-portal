using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Context;
using Infrastructure.Entity;

namespace DAL.Entities
{
    public class User : Applicant, IIdentityEntity<int>, ISoftDeleteEntity
    {

        [NotMapped]
        public override string TableName { get; } = nameof(JobPortalDbContext.Users);

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual List<SkillTag> Skills { get; set; }

        public DateTime? DeletedDate { get; set; }

    }
}