﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Context;
using Infrastructure.Entity;

namespace DAL.Entities
{
    public class User : Applicant, IIdentityEntity
    {

        [NotMapped]
        public override string TableName { get; } = nameof(JobPortalDbContext.Users);
        public virtual List<SkillTag> Skills { get; set; }

        [Required, StringLength(100)]
        public string PasswordSalt { get; set; }

        [Required, StringLength(100)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// String with , delimiter.
        /// For example: "Admin,Editor,Tutor"
        /// </summary>
        public string Roles { get; set; }
    }
}