using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class JobOffer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int EmployerId { get; set; }

        [Required]
        public Employer Employer { get; set; }

        public Address Location { get; set; }

        public string Description { get; set; }

        public virtual List<SkillTag> Skills { get; set; }

        public virtual List<Question> Questions { get; set; }

        public virtual List<JobApplication> Applications { get; set; }
    }
}