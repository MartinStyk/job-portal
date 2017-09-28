using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Employer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Address Address { get; set; }

        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public virtual List<JobOffer> JobOffers { get; set; }
    }
}