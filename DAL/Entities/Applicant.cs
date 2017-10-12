using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using Infrastructure.Entity;

namespace DAL.Entities
{
    public class Applicant : IEntity<int>
    {
        public int Id { get; set; }

        [NotMapped]
        public virtual string TableName { get; } = nameof(JobPortalDbContext.Applicants);

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Education { get; set; }

        public virtual List<JobApplication> JobApplications { get; set; }
    }
}