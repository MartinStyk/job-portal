using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class SkillTag
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<JobOffer> JobOffers { get; set; }

        public virtual List<User> Users { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}